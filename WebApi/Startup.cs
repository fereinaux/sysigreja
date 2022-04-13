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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            services.AddCors();


            services.AddControllers();
            services.AddScoped(_ => new ApplicationDbContext(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(_ => new ConsultaDbContext(configuration.GetConnectionString("ConsultaConnection")));
            services.AddTransient<IParticipantesBusiness, ParticipantesBusiness>();
            services.AddTransient<IGenericRepository<Evento>, GenericRepository<Evento>>();
            services.AddTransient<IGenericRepository<ApplicationUser>, GenericRepository<ApplicationUser>>();
            services.AddTransient<IGenericRepository<Participante>, GenericRepository<Participante>>();
            services.AddTransient<IGenericRepository<Newsletter>, GenericRepository<Newsletter>>();
            services.AddTransient<IGenericRepository<MeioPagamento>, GenericRepository<MeioPagamento>>();
            services.AddTransient<IGenericRepository<ReuniaoEvento>, GenericRepository<ReuniaoEvento>>();
            services.AddTransient<IGenericRepository<Equipante>, GenericRepository<Equipante>>();
            services.AddTransient<IGenericRepository<EquipanteEvento>, GenericRepository<EquipanteEvento>>();
            services.AddTransient<IGenericRepository<PresencaReuniao>, GenericRepository<PresencaReuniao>>();
            services.AddTransient<IGenericRepository<ContaBancaria>, GenericRepository<ContaBancaria>>();
            services.AddTransient<IGenericRepository<CentroCusto>, GenericRepository<CentroCusto>>();
            services.AddTransient<IGenericRepository<Lancamento>, GenericRepository<Lancamento>>();
            services.AddTransient<IGenericRepository<Arquivo>, GenericRepository<Arquivo>>();
            services.AddTransient<IGenericRepository<Circulo>, GenericRepository<Circulo>>();
            services.AddTransient<IGenericRepository<Quarto>, GenericRepository<Quarto>>();
            services.AddTransient<IGenericRepository<CirculoParticipante>, GenericRepository<CirculoParticipante>>();
            services.AddTransient<IGenericRepository<QuartoParticipante>, GenericRepository<QuartoParticipante>>();
            services.AddTransient<IGenericRepositoryConsulta<ParticipanteConsulta>, GenericRepositoryConsulta<ParticipanteConsulta>>();

            services.AddTransient<IContaBancariaBusiness, ContaBancariaBusiness>();
            services.AddTransient<IArquivosBusiness, ArquivosBusiness>();
            services.AddTransient<ILancamentoBusiness, LancamentoBusiness>();
            services.AddTransient<ICentroCustoBusiness, CentroCustoBusiness>();
            services.AddTransient<IEquipantesBusiness, EquipantesBusiness>();
            services.AddTransient<IEquipesBusiness, EquipesBusiness>();
            services.AddTransient<IEventosBusiness, EventosBusiness>();
            services.AddTransient<INewsletterBusiness, NewsletterBusiness>();
            services.AddTransient<IParticipantesBusiness, ParticipantesBusiness>();
            services.AddTransient<IMeioPagamentoBusiness, MeioPagamentoBusiness>();
            services.AddTransient<IAccountBusiness, AccountBusiness>();
            services.AddTransient<IReunioesBusiness, ReunioesBusiness>();
            services.AddTransient<ICirculosBusiness, CirculosBusiness>();
            services.AddTransient<IQuartosBusiness, QuartosBusiness>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option => option.AllowAnyOrigin()); ;

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
