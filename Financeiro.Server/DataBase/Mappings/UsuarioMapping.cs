using Financeiro.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financeiro.Server.DataBase.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuário");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired(true)
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired(true)
                .HasMaxLength(250);

            builder.Property(x => x.Senha)
                .IsRequired(true)
                .HasMaxLength(20);
        }
    }
}
