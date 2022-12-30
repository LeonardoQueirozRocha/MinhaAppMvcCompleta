using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(produto => produto.Id);

            builder.Property(produto => produto.Nome)
                .IsRequired()
                .HasColumnType("NVARCHAR(200)");

            builder.Property(produto => produto.Descricao)
                .IsRequired()
                .HasColumnType("NVARCHAR(1000)");

            builder.Property(produto => produto.Imagem)
                .IsRequired()
                .HasColumnType("NVARCHAR(100)");

            builder.ToTable("Produtos");
        }
    }
}
