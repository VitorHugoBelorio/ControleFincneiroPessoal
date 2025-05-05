using Financeiro.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Server.DataBase.Mappings
{
    public class TransacaoMapping : IEntityTypeConfiguration<Transacao>
    {
        public void Configure(EntityTypeBuilder<Transacao> builder)
        {
            builder.ToTable("Transação");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Titulo)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.CriadoEm)
                .IsRequired(true);

            builder.Property(x => x.PagoOuRecebidoEm)
                .IsRequired(false);

            builder.Property(x => x.Type)
                .IsRequired(true);

            builder.Property(x => x.Quantia)
                .HasColumnType("DECIMAL(18,2)")
                .IsRequired(true);

            builder.Property(x => x.UserId)
                .IsRequired(true);

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId);
        }
    }
}
