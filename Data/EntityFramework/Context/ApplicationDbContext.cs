using Data.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using Utils.Enums;
using Utils.Extensions;

namespace Data.Context
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public PerfisUsuarioEnum Perfil { get; set; }
        public StatusEnum Status { get; set; }
        public string Senha { get; set; }
        public int? EquipanteId { get; set; }
        public virtual Equipante Equipante { get; set; }
    }

    public class ConsultaDbContext: DbContext
    {
        public ConsultaDbContext(string connection)
    : base(connection)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
        public ConsultaDbContext()
       : base("ConsultaConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<ParticipanteConsulta> Participantes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(string connection)
           : base(connection)
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Circulo> Circulos { get; set; }
        public DbSet<Quarto> Quartos { get; set; }
        public DbSet<QuartoParticipante> QuartoParticipantes { get; set; }
        public DbSet<CirculoParticipante> CirculoParticipantes { get; set; }
        public DbSet<ParticipantesEtiquetas> ParticipantesEtiquetas { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<Equipante> Equipantes { get; set; }
        public DbSet<EquipanteEvento> EquipantesEventos { get; set; }
        public DbSet<ReuniaoEvento> ReunioesEventos { get; set; }
        public DbSet<Configuracao> Configuracoes { get; set; }
        public DbSet<ConfiguracaoCampos> ConfiguracaoCampos { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<PresencaReuniao> PresencaReunioes { get; set; }
        public DbSet<MeioPagamento> MeioPagamentos { get; set; }
        public DbSet<ContaBancaria> ContasBancarias { get; set; }
        public DbSet<CentroCusto> CentroCustos { get; set; }
        public DbSet<Lancamento> Lancamentos { get; set; }
        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Circulo>()
                .HasOptional<Evento>(c => c.Evento)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Circulo>()
                .HasRequired<Evento>(c => c.Evento)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Circulo>()
                .HasOptional<EquipanteEvento>(c => c.Dirigente1)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Circulo>()
                .HasOptional<EquipanteEvento>(c => c.Dirigente2)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EquipanteEvento>()
                .HasOptional<Evento>(e => e.Evento)
                .WithOptionalDependent()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quarto>()
               .HasOptional<Evento>(q => q.Evento)
               .WithOptionalDependent()
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<Participante>()
                .HasMany(x => x.Arquivos);

            modelBuilder.Entity<Participante>()
                .HasMany(x => x.Circulos);

            modelBuilder.Entity<Equipante>()
                .HasMany(x => x.Arquivos);

            modelBuilder.Entity<Equipante>()
                .HasMany(x => x.Equipes);


            modelBuilder.Entity<EquipanteEvento>()
                .HasMany(x => x.Presencas);


        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}