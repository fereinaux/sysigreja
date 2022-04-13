namespace Data.Migrations
{
    using Data.Context;
    using Data.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;
    using Utils.Constants;
    using Utils.Enums;
    using Utils.Extensions;

    public sealed class ConsultaConfiguration : DbMigrationsConfiguration<ConsultaDbContext>
    {
        public ConsultaConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Data.Context.ApplicationDbContext context)
        {
            if (!context.Configuracoes.Any())
            {

                context.Configuracoes.AddOrUpdate(x => x.Id,
                  new Data.Entities.Configuracao
                  {
                      Id = 1,
                      Titulo = "SysIgreja",
                      CorBotao = "#5f6567",
                      CorLoginBox = "#1b1918",
                      CorHoverBotao = "#424648",
                      CorScroll = "#2d2f2f",
                      CorHoverScroll = "#0a0a0a",
                      TipoCirculo = TipoCirculoEnum.Aleatorio
                  });

                context.ConfiguracaoCampos.AddOrUpdate(x => x.Campo,
                    new Data.Entities.ConfiguracaoCampos { Campo = CamposEnum.Nome },
                    new Data.Entities.ConfiguracaoCampos { Campo = CamposEnum.Apelido },
                    new Data.Entities.ConfiguracaoCampos { Campo = CamposEnum.DataNascimento },
                    new Data.Entities.ConfiguracaoCampos { Campo = CamposEnum.Email },
                    new Data.Entities.ConfiguracaoCampos { Campo = CamposEnum.Fone }
                );
            }

            context.MeioPagamentos.AddOrUpdate(x => x.Descricao,
              new Data.Entities.MeioPagamento { Descricao = MeioPagamentoPadraoEnum.Dinheiro.GetDescription(), Taxa = 0, IsEditavel = false, Status = StatusEnum.Ativo },
              new Data.Entities.MeioPagamento { Descricao = MeioPagamentoPadraoEnum.Transferencia.GetDescription(), Taxa = 0, IsEditavel = false, Status = StatusEnum.Ativo },
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

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
