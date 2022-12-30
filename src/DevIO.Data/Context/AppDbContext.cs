using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            var properties = modelBuilder.Model.GetEntityTypes().SelectMany(entity => entity.GetProperties()).Where(property => property.ClrType == typeof(string));

            foreach (var property in properties) property.SetColumnType("VARCHAR(100)");

            var relationships = modelBuilder.Model.GetEntityTypes().SelectMany(entity => entity.GetForeignKeys());

            foreach (var relatoinship in relationships) relatoinship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
