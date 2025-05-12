using Financeiro.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Financeiro.Server.DataBase
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // Aqui você pode definir suas DbSets
        // public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;
    }
}
