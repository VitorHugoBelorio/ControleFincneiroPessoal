using Financeiro.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Server.DataBase.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");

            builder.HasKey(x => x.Id); // chave primária

            builder.Property(x => x.Titulo)
                .IsRequired(true)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Descricao)
                .IsRequired(false)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255);

            builder.Property(x => x.UserId)
                .IsRequired(true);
        }

    }
}
