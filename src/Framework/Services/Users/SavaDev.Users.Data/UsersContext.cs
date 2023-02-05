using Microsoft.EntityFrameworkCore;
using SavaDev.Base.Data.Context;
using SavaDev.Base.User.Data.Context;
using SavaDev.Base.User.Data.Entities;

namespace SavaDev.Users.Data
{
    // Конечная реализация для проектов, где нужен минимальный функционал для пользователей
    public class UsersContext : BaseUserDbContext<User, Role>, IDbContext
    {

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {

        }

        public DbSet<AuthToken> AuthTokens { get; set; }

        public DbSet<Lockout> Lockouts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureContext(_dbOptionsExtension);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
