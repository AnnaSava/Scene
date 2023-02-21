using Framework.User.DataService.Entities;
using Microsoft.EntityFrameworkCore;

namespace Framework.User.DataService.Contract.Interfaces
{
    public interface IAuthTokenContext 
    {
        DbSet<AuthToken> AuthTokens { get; set; }
    }
}
