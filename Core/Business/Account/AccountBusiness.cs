using Core.Models.Eventos;
using Data.Context;
using Data.Entities;
using Data.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Utils.Constants;
using Utils.Enums;
using Utils.Extensions;

namespace Core.Business.Account
{
    public class AccountBusiness : IAccountBusiness
    {
        private readonly IGenericRepository<ApplicationUser> accountRepository;
        private readonly IGenericRepository<Equipante> equipanteRepository;
        private readonly ApplicationDbContext context;

        public AccountBusiness(IGenericRepository<ApplicationUser> accountRepository, IGenericRepository<Equipante> equipanteRepository, ApplicationDbContext context)
        {
            this.accountRepository = accountRepository;            
            this.equipanteRepository = equipanteRepository;            
            this.context = context;            
        }

        public List<Equipante> GetEquipantesUsuario(string idUsuario)
        {
            var equipantesIds = accountRepository.GetAll(x => x.EquipanteId != null && x.Id != idUsuario).Select(y => y.EquipanteId).ToList();

            return equipanteRepository.GetAll().ToList().Where(x => !equipantesIds.Contains(x.Id)).ToList();
        }

        public ApplicationUser GetUsuarioById(string id)
        {
            return accountRepository.GetById(id);
        }

        public IQueryable<ApplicationUser> GetUsuarios()
        {
            return accountRepository.GetAll(x => x.Status != StatusEnum.Deletado);
        }

        public void ToggleUsuarioStatus(string id)
        {
            ApplicationUser usuario = accountRepository.GetById(id);

            StatusEnum status = usuario.Status == StatusEnum.Ativo ?
                StatusEnum.Inativo :
                StatusEnum.Ativo;

            usuario.Status = status;

            accountRepository.Update(usuario);
            accountRepository.Save();
        }


        public void DeleteUsuario(string id)
        {
            ApplicationUser usuario = accountRepository.GetById(id);

            usuario.Status = StatusEnum.Deletado;

            accountRepository.Update(usuario);
            accountRepository.Save();
        }
        public void Seed()
        {
            context.MeioPagamentos.AddOrUpdate(x => x.Descricao,
              new Data.Entities.MeioPagamento { Descricao = MeioPagamentoPadraoEnum.Transferencia.GetDescription(), Taxa = 0, IsEditavel = false, Status = StatusEnum.Ativo },
              new Data.Entities.MeioPagamento { Descricao = MeioPagamentoPadraoEnum.Dinheiro.GetDescription(), Taxa = 0, IsEditavel = false, Status = StatusEnum.Ativo },
              new Data.Entities.MeioPagamento { Descricao = MeioPagamentoPadraoEnum.Isencao.GetDescription(), Taxa = 0, IsEditavel = false, Status = StatusEnum.Ativo }
            );

            context.CentroCustos.AddOrUpdate(x => x.Id,
              new Data.Entities.CentroCusto { Id = (int)CentroCustoPadraoEnum.Inscricoes, Descricao = CentroCustoPadraoEnum.Inscricoes.GetDescription(), Tipo = TiposCentroCustoEnum.Receita },
              new Data.Entities.CentroCusto { Id = (int)CentroCustoPadraoEnum.TaxaEquipante, Descricao = CentroCustoPadraoEnum.TaxaEquipante.GetDescription(), Tipo = TiposCentroCustoEnum.Receita }
            );

            ApplicationUser master = new ApplicationUser
            {
                Id = Usuario.MasterId,
                Email = "felipereinaux@gmail.com",
                UserName = "master",
                PasswordHash = "AMYItPuKcpqwK3/O+FMtVYMpwXnAT1+txuT/rxT8s6eOcHKML4AbRbS2S7JJOg/E1w==",
                SecurityStamp = "d857dd21-e90e-4f09-897f-1dc8532a461e",
                Senha = "master",
                Status = StatusEnum.Ativo,
                Perfil = PerfisUsuarioEnum.Master,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            context.Users.AddOrUpdate(x => x.Id, master);

            context.Roles.AddOrUpdate(x => x.Name,
                 new IdentityRole { Name = PerfisUsuarioEnum.Master.GetDescription() },
                 new IdentityRole { Name = PerfisUsuarioEnum.Admin.GetDescription() },
                 new IdentityRole { Name = PerfisUsuarioEnum.Secretaria.GetDescription() },
                 new IdentityRole { Name = PerfisUsuarioEnum.Coordenador.GetDescription() }
                 );

            context.SaveChanges();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            UserManager.AddToRole(master.Id, PerfisUsuarioEnum.Master.GetDescription());

            context.SaveChanges();
        }
    }
}
