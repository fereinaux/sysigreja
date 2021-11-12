
using Core.Business.ContaBancaria;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Newsletter;
using Core.Business.Participantes;
using Core.Models.Lancamento;
using Core.Models.Participantes;
using Data.Entities;
using SysIgreja.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml;
using Utils.Enums;
using Utils.Extensions;

namespace SysIgreja.Controllers
{
    public class InscricoesController : Controller
    {
        private readonly IParticipantesBusiness participantesBusiness;
        private readonly ILancamentoBusiness lancamentoBusiness;
        private readonly IMeioPagamentoBusiness meioPagamentoBusiness;
        private readonly IContaBancariaBusiness contaBancariaBusiness;
        private readonly IEventosBusiness eventosBusiness;
        private readonly INewsletterBusiness newsletterBusiness;

        public InscricoesController(IParticipantesBusiness participantesBusiness, IContaBancariaBusiness contaBancariaBusiness, IEventosBusiness eventosBusiness, INewsletterBusiness newsletterBusiness, ILancamentoBusiness lancamentoBusiness, IMeioPagamentoBusiness meioPagamentoBusiness)
        {
            this.participantesBusiness = participantesBusiness;
            this.meioPagamentoBusiness = meioPagamentoBusiness;
            this.lancamentoBusiness = lancamentoBusiness;
            this.contaBancariaBusiness = contaBancariaBusiness;
            this.eventosBusiness = eventosBusiness;
            this.newsletterBusiness = newsletterBusiness;
        }

        public ActionResult Index()
        {


            ViewBag.Title = "Inscrições";
            var evento = eventosBusiness.GetEventoAtivo();
            if (evento == null)
                return RedirectToAction("InscricoesEncerradas");
            ViewBag.Logo = evento.TipoEvento.GetNickname() + ".png";
            return View();
        }

        private bool CapacidadeUltrapassada(Evento evento, StatusEnum[] arrStatus)
        {
            return participantesBusiness
                            .GetParticipantesByEvento(evento.Id)
                            .Where(x => (arrStatus).Contains(x.Status))
                            .Count() >= evento.Capacidade;
        }

        public ActionResult InscricaoConcluida(int Id)
        {
            Participante participante = participantesBusiness.GetParticipanteById(Id);

            ViewBag.Participante = new InscricaoConcluidaViewModel
            {
                Id = participante.Id,
                Apelido = participante.Apelido,
                Logo = participante.Evento.TipoEvento.GetNickname() + ".png",
                Evento = $"{participante.Evento.Numeracao.ToString()}º {participante.Evento.TipoEvento.GetDescription()}",
                Valor = participante.Evento.Valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")),
                DataEvento = participante.Evento.DataEvento.ToString("dd/MM/yyyy")
            };

            ViewBag.ContasBancarias = contaBancariaBusiness.GetContasBancarias().ToList()
               .Select(x => new ContaBancariaViewModel
               {
                   Id = x.Id,
                   Banco = x.Banco.GetDescription(),
                   Agencia = x.Agencia,
                   CPF = x.CPF,
                   Conta = x.Conta,
                   Nome = x.Nome,
                   Operacao = x.Operacao
               });


            if (participante.Status == StatusEnum.Inscrito)
            {

                return View("InscricaoConcluida");
            }
            else
                return View("InscricaoCompleta");
        }

        public ActionResult InscricaoEspera(int Id)
        {
            Participante participante = participantesBusiness.GetParticipanteById(Id);

            ViewBag.Participante = new InscricaoConcluidaViewModel
            {
                Id = participante.Id,
                Apelido = participante.Apelido,
                Logo = participante.Evento.TipoEvento.GetNickname() + ".png",
                Evento = $"{participante.Evento.Numeracao.ToString()}º {participante.Evento.TipoEvento.GetDescription()}",
                Valor = participante.Evento.Valor.ToString("C", CultureInfo.CreateSpecificCulture("pt-BR")),
                DataEvento = participante.Evento.DataEvento.ToString("dd/MM/yyyy")
            };


            return View("InscricaoEspera");
        }

        public ActionResult InscricoesEncerradas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostInscricao(PostInscricaoModel model)
        {
            AddNewsletter(model.Fone);


            var evento = eventosBusiness.GetEventoAtivo();
            model.EventoId = model.EventoId > 0 ? model.EventoId : evento.Id;

            if (evento != null && participantesBusiness.GetParticipantesByEvento(model.EventoId).Where(x => x.Status != StatusEnum.Cancelado).Count() >= evento.Capacidade)
            {
                model.Status = "Espera";

                return Json(Url.Action("InscricaoEspera", new { Id = participantesBusiness.PostInscricao(model) }));
            }

            return Json(Url.Action("InscricaoConcluida", new { Id = participantesBusiness.PostInscricao(model) }));
        }

        [HttpPost]
        public ActionResult Checkin(PostInscricaoModel model)
        {
            return Json(new { Id = participantesBusiness.PostInscricao(model) });
        }

        [HttpPost]
        public ActionResult Newsletter(string Whatsapp)
        {
            AddNewsletter(Whatsapp);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult VerificaCadastro(string Email)
        {
            var participante = participantesBusiness.GetParticipantesByEvento(eventosBusiness.GetEventoAtivo().Id).FirstOrDefault(x => x.Email == Email && (new StatusEnum[] { StatusEnum.Confirmado, StatusEnum.Inscrito }).Contains(x.Status));

            if (participante != null)
                return Json(Url.Action("InscricaoConcluida", new { Id = participante.Id }));

            var participanteConsulta = participantesBusiness.GetParticipanteConsulta(Email);

            if (participanteConsulta != null)
                return Json(new { Participante = participanteConsulta }, JsonRequestBehavior.AllowGet);

            return new HttpStatusCodeResult(200);
        }

        [HttpPost]
        public ActionResult SolicitarBoleto(int ParticipanteId)
        {
            participantesBusiness.SolicitarBoleto(ParticipanteId);

            return new HttpStatusCodeResult(200);
        }

        private void AddNewsletter(string email)
        {
            newsletterBusiness.InsertWhatsapp(email);
        }

        public async Task<ActionResult> CheckoutPagSeguro(int Id)
        {

            Participante participante = participantesBusiness.GetParticipanteById(Id);

            if (CapacidadeUltrapassada(participante.Evento, new StatusEnum[] { StatusEnum.Confirmado }))
                return View("InscricoesEncerradas");

            //URI de checkout.
            string uri = @"https://ws.pagseguro.uol.com.br/v2/checkout";

            //Webclient faz o post para o servidor de pagseguro.
            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("email", participante.Evento.TipoEvento.GetEmailPagseguro()),
                new KeyValuePair<string, string>("token", participante.Evento.TipoEvento.GetTokenPagseguro()),
                new KeyValuePair<string, string>("currency", "BRL"),
                new KeyValuePair<string, string>("itemId1", "0001"),
                new KeyValuePair<string, string>("itemDescription1", $"Inscrição {participante.Evento.Numeracao.ToString()}º {participante.Evento.TipoEvento.GetDescription()}"),
                new KeyValuePair<string, string>("itemAmount1", participante.Evento.Valor.ToString("0.00").Replace(",", ".")),
                new KeyValuePair<string, string>("itemQuantity1", "1"),
                new KeyValuePair<string, string>("itemWeight1", "0"),
                new KeyValuePair<string, string>("reference", participante.ReferenciaPagSeguro),
                new KeyValuePair<string, string>("senderName", participante.Nome),
                new KeyValuePair<string, string>("senderAreaCode", participante.Fone.Substring(4, 2)),
                new KeyValuePair<string, string>("senderPhone", participante.Fone.Substring(7, 11).Replace(".", "").Replace("-", "")),
                new KeyValuePair<string, string>("senderEmail", participante.Email),
                new KeyValuePair<string, string>("shippingAddressRequired", "false")
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            content.Headers.ContentType.CharSet = "UTF-8";

            Task<HttpResponseMessage> response = client.PostAsync(uri, content);

            //Cria documento XML.
            XmlDocument xmlDoc = new XmlDocument();

            //Carrega documento XML por string.
            xmlDoc.LoadXml(await response.Result.Content.ReadAsStringAsync());

            //Obtém código de transação (Checkout).
            var code = xmlDoc.GetElementsByTagName("code")[0];

            //Obtém data de transação (Checkout).
            var date = xmlDoc.GetElementsByTagName("date")[0];

            //Monta a URL para pagamento.
            var paymentUrl = string.Concat("https://pagseguro.uol.com.br/v2/checkout/payment.html?code=", code.InnerText);

            return Redirect(paymentUrl);
        }

        [HttpPost]
        [AllowAnonymous]
        public void RetornoPagSeguro(string notificationCode, string notificationType)
        {
            var evento = eventosBusiness.GetEventoAtivo();
            //uri de consulta da transação.
            string uri = $"https://ws.pagseguro.uol.com.br/v3/transactions/notifications/{notificationCode}?email={evento.TipoEvento.GetEmailPagseguro()}&token={evento.TipoEvento.GetTokenPagseguro()}";

            //Classe que irá fazer a requisição GET.
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);

            //Método do webrequest.
            request.Method = "GET";

            //String que vai armazenar o xml de retorno.
            string xmlString = null;

            //Obtém resposta do servidor.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                //Cria stream para obter retorno.
                using (Stream dataStream = response.GetResponseStream())
                {
                    //Lê stream.
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        //Xml convertido para string.
                        xmlString = reader.ReadToEnd();

                        //Cria xml document para facilitar acesso ao xml.
                        XmlDocument xmlDoc = new XmlDocument();

                        //Carrega xml document através da string com XML.
                        xmlDoc.LoadXml(xmlString);

                        //Busca elemento status do XML.
                        var status = xmlDoc.GetElementsByTagName("status")[0];

                        //Fecha reader.
                        reader.Close();

                        //Fecha stream.
                        dataStream.Close();

                        //Verifica status de retorno.
                        //3 = Pago.
                        //if (status.InnerText == "3")
                        //{
                        //    var reference = xmlDoc.GetElementsByTagName("reference")[0];
                        //    var participante = participantesBusiness.GetParticipanteByReference(reference.InnerText);
                        //    var meioPagSeguro = MeioPagamentoPadraoEnum.PagSeguro.GetDescription();
                        //    lancamentoBusiness.PostPagamento(new PostPagamentoModel
                        //    {
                        //        ParticipanteId = participante.Id,
                        //        Valor = evento.Valor,
                        //        MeioPagamentoId = meioPagamentoBusiness.GetAllMeioPagamentos().FirstOrDefault(x => x.Descricao == meioPagSeguro).Id,
                        //        EventoId = evento.Id
                        //    });
                        //}

                    }
                }
            }
        }
    }
}