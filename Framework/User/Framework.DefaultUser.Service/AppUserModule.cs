using AutoMapper;
using Framework.Base.Service.Module;
using Framework.DefaultUser.Data.Contract;
using Framework.DefaultUser.Data.Services;
using Framework.DefaultUser.Service.Contract;
using Framework.DefaultUser.Service.Services;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Services;
using Framework.User.Service.Taskers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.User.Service
{
    public static class AppUserModule
    {
        public static void AddAppUser(this IServiceCollection services, IConfiguration config)
        {
            services.AddModuleDbContext<AppUserContext>(config, new ModuleSettings("Ap", "IdentityConnection"));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppUserContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });

            services.AddScoped<IUserManagerAdapter<AppUser>>(s => new UserManagerAdapter<AppUser>(
                s.GetService<UserManager<AppUser>>()));

            services.AddScoped<IRoleManagerAdapter<AppRole>>(s => new RoleManagerAdapter<AppRole>(
               s.GetService<RoleManager<AppRole>>()));

            services.AddScoped<ISignInManagerAdapter>(s => new SignInManagerAdapter<AppUser>(
                s.GetService<UserManager<AppUser>>(),
                s.GetService<SignInManager<AppUser>>()));

            services.AddScoped<IAppUserDbService>(s => new AppUserDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IUserManagerAdapter<AppUser>>(),
                s.GetService<ISignInManagerAdapter>(),
                s.GetService<IMapper>()));

            services.AddScoped<IAppAccountDbService>(s => new AppAccountDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IUserManagerAdapter<AppUser>>(),
                s.GetService<ISignInManagerAdapter>(),
                s.GetService<IMapper>()));

            services.AddScoped<IReservedNameDbService>(s => new ReservedNameDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IPermissionDbService>(s => new PermissionDbService(s.GetService<AppUserContext>(), s.GetService<IMapper>()));

            services.AddScoped<IAppRoleDbService>(s => new AppRoleDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IRoleManagerAdapter<AppRole>>(),
                s.GetService<IMapper>()));

            services.AddScoped<IAuthDbService>(s => new AuthDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IMapper>()));

            var cultures = config["Cultures"].Split(',');

            services.AddScoped<ILegalDocumentDbService>(s => new LegalDocumentDbService(
                s.GetService<AppUserContext>(),
                cultures,
                s.GetService<IMapper>()));

            services.AddScoped<IAppAccountService, AppAccountService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppRoleService, AppRoleService>();
            services.AddScoped<IReservedNameService, ReservedNameService>();
            services.AddScoped<ILegalDocumentService, LegalDocumentService>();
        }
    }
}
