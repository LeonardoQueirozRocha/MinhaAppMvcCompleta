using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevIO.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(endereco => endereco.Id);

            builder.Property(endereco => endereco.Logradouro)
                .IsRequired()
                .HasColumnType("VARCHAR(200)");

            builder.Property(endereco => endereco.Numero)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.Property(endereco => endereco.Cep)
                .IsRequired()
                .HasColumnType("VARCHAR(8)");

            builder.Property(endereco => endereco.Complemento)
                .HasColumnType("VARCHAR(250)");

            builder.Property(endereco => endereco.Bairro)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(endereco => endereco.Cidade)
                .IsRequired()
                .HasColumnType("VARCHAR(100)");

            builder.Property(endereco => endereco.Estado)
                .IsRequired()
                .HasColumnType("VARCHAR(50)");

            builder.ToTable("Enderecos");
        }
    }
}
