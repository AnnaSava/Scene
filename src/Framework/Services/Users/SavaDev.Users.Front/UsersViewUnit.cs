using Framework.DefaultUser.Service.Services;
using Framework.User.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Unit;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.User.Data.Services;
using SavaDev.Base.User.Data.Services.Interfaces;
using SavaDev.Base.Users.Security;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Services;
using SavaDev.Users.Data;
using SavaDev.Users.Data.Entities;
using SavaDev.Users.Front.Contract;
using SavaDev.Users.Front.Security;

namespace SavaDev.Users.Front
{
    public static class UsersViewUnit
    {
        public static void AddUsers(this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
        {
            services.AddUnitDbContext<IDbContext, UsersContext>(config, unitOptions);

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<UsersContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAccountService, WebAccountService>();
            services.AddScoped<ISecurityService, WebSecurityService>();

            services.AddScoped<IReservedNameService, ReservedNameService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ILegalDocumentService, LegalDocumentService>();

            services.AddScoped<IAuthDbService, AuthDbService>();

            services.AddScoped<IAccountViewService, AccountViewService>();
            //services.AddScoped<IUserViewService, UserViewService>();
            services.AddScoped<IRoleViewService, RoleViewService>();
        }
    }
}
