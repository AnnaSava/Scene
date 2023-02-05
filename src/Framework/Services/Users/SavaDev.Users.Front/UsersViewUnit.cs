using AutoMapper;
using Framework.DefaultUser.Service.Contract;
using Framework.DefaultUser.Service.Services;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SavaDev.Base.Data.Context;
using SavaDev.Base.Unit.Options;
using SavaDev.Base.Unit;
using SavaDev.Base.User.Data.Services;
using SavaDev.Base.User.Data.Services.Interfaces;
using SavaDev.Base.Users.Security.Account;
using SavaDev.Base.Users.Security.Contract;
using SavaDev.System.Data.Contract;
using SavaDev.System.Data.Services;
using SavaDev.Users.Data;

namespace SavaDev.Users.Front
{
    public static class UsersViewUnit
    {
        public static void AddUsers (this IServiceCollection services, IConfiguration config, UnitOptions unitOptions)
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

            services.AddScoped<IAccountService, AccountService<long, User>>();

            services.AddScoped<IReservedNameService, ReservedNameService>();

            services.AddScoped<IPermissionService, PermissionService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IAuthDbService, AuthDbService>();

            services.AddScoped<ILegalDocumentService, LegalDocumentService >();

            services.AddScoped<IAccountViewService, AccountViewService>();
            //services.AddScoped<IUserViewService, UserViewService>();
            services.AddScoped<IRoleViewService, RoleViewService>();
        }
    }
}
