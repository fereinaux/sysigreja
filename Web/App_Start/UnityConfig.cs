using Core.Business.Account;
using Core.Business.Arquivos;
using Core.Business.CentroCusto;
using Core.Business.Circulos;
using Core.Business.ContaBancaria;
using Core.Business.Equipantes;
using Core.Business.Equipes;
using Core.Business.Eventos;
using Core.Business.Lancamento;
using Core.Business.MeioPagamento;
using Core.Business.Newsletter;
using Core.Business.Participantes;
using Core.Business.Quartos;
using Core.Business.Reunioes;
using Data.Context;
using Data.Entities;
using Data.Repository;
using SysIgreja.Controllers;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;
using Utils.Services;

namespace Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();           
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            container.RegisterType<IGenericRepositoryConsulta<ParticipanteConsulta>, GenericRepositoryConsulta<ParticipanteConsulta>>();
            container.RegisterType<IGenericRepository<Evento>, GenericRepository<Evento>>();
            container.RegisterType<IGenericRepository<ApplicationUser>, GenericRepository<ApplicationUser>>();
            container.RegisterType<IGenericRepository<Participante>, GenericRepository<Participante>>();
            container.RegisterType<IGenericRepository<Newsletter>, GenericRepository<Newsletter>>();
            container.RegisterType<IGenericRepository<MeioPagamento>, GenericRepository<MeioPagamento>>();
            container.RegisterType<IGenericRepository<ReuniaoEvento>, GenericRepository<ReuniaoEvento>>();
            container.RegisterType<IGenericRepository<Equipante>, GenericRepository<Equipante>>();
            container.RegisterType<IGenericRepository<EquipanteEvento>, GenericRepository<EquipanteEvento>>();
            container.RegisterType<IGenericRepository<PresencaReuniao>, GenericRepository<PresencaReuniao>>();
            container.RegisterType<IGenericRepository<ContaBancaria>, GenericRepository<ContaBancaria>>();
            container.RegisterType<IGenericRepository<CentroCusto>, GenericRepository<CentroCusto>>();
            container.RegisterType<IGenericRepository<Lancamento>, GenericRepository<Lancamento>>();
            container.RegisterType<IGenericRepository<Arquivo>, GenericRepository<Arquivo>>();
            container.RegisterType<IGenericRepository<Circulo>, GenericRepository<Circulo>>();
            container.RegisterType<IGenericRepository<Quarto>, GenericRepository<Quarto>>();
            container.RegisterType<IGenericRepository<CirculoParticipante>, GenericRepository<CirculoParticipante>>();
            container.RegisterType<IGenericRepository<QuartoParticipante>, GenericRepository<QuartoParticipante>>();

            container.RegisterType<IEmailSender, EmailSender>();
            container.RegisterType<IDatatableService, DatatableService>();

            container.RegisterType<IContaBancariaBusiness, ContaBancariaBusiness>();
            container.RegisterType<IArquivosBusiness, ArquivosBusiness>();
            container.RegisterType<ILancamentoBusiness, LancamentoBusiness>();
            container.RegisterType<ICentroCustoBusiness, CentroCustoBusiness>();
            container.RegisterType<IEquipantesBusiness, EquipantesBusiness>();
            container.RegisterType<IEquipesBusiness, EquipesBusiness>();
            container.RegisterType<IEventosBusiness, EventosBusiness>();
            container.RegisterType<INewsletterBusiness, NewsletterBusiness>();
            container.RegisterType<IParticipantesBusiness, ParticipantesBusiness>();
            container.RegisterType<IMeioPagamentoBusiness, MeioPagamentoBusiness>();
            container.RegisterType<IAccountBusiness, AccountBusiness>();
            container.RegisterType<IReunioesBusiness, ReunioesBusiness>();
            container.RegisterType<ICirculosBusiness, CirculosBusiness>();
            container.RegisterType<IQuartosBusiness, QuartosBusiness>();
        }
    }
}