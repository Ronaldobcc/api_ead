using cadastro.Data.Map;
using cadastro.Models;
using Microsoft.EntityFrameworkCore;

namespace cadastro.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) :
            base(options)
        {
        }

        public virtual DbSet<ContatoModel> Contatos { get; set; }
        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<AulaModel> Aulas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.ApplyConfiguration(new ContatoMap());
                modelBuilder.Entity(entity.Name).Property("Nome").HasColumnType("varchar(255)").IsUnicode(false);
                modelBuilder.Entity<AulaModel>().Property(a => a.Descricao).HasColumnType("varchar(1000)").IsUnicode(false);
                modelBuilder.Entity<AulaModel>().Property(a => a.VideoUrl).HasColumnType("varchar(255)").IsUnicode(false);
                modelBuilder.Entity<AulaModel>().Property(a => a.CapaUrl).HasColumnType("varchar(255)").IsUnicode(false);
                base.OnModelCreating(modelBuilder);

            }
            
        }
    }
}

