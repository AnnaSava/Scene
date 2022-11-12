using AutoMapper;
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
        public static void AddFrameworkUser(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppUserContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });

            services.AddScoped<IUserManagerAdapter<AppUser>>(s => new UserManagerAdapter<AppUser>(
                s.GetService<UserManager<AppUser>>()));

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

            services.AddScoped<IAppFrameworkUserService>(s => new AppUserService(
                s.GetService<IAppUserDbService>(),
                s.GetService<IAppAccountDbService>(),
                s.GetService<ISignInManagerAdapter>(),
                s.GetService<IReservedNameDbService>(),
                s.GetService<RegisterTasker>(),
                s.GetService<IMapper>()));

            services.AddScoped<IAppAccountService>(s => new AppAccountService(
                s.GetService<IAppUserDbService>(),
                s.GetService<IAppAccountDbService>(),
                s.GetService<ISignInManagerAdapter>(),
                s.GetService<IReservedNameDbService>(),
                s.GetService<RegisterTasker>(),
                s.GetService<IMapper>()));

            services.AddScoped<IPermissionDbService>(s => new PermissionDbService(s.GetService<AppUserContext>(), s.GetService<IMapper>()));
            services.AddScoped<IPermissionService>(s => new PermissionService(
                s.GetService<IPermissionDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<IRoleManagerAdapter<AppRole>>(s => new RoleManagerAdapter<AppRole>(
                s.GetService<RoleManager<AppRole>>()));

            services.AddScoped<IAppRoleDbService>(s => new AppRoleDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IRoleManagerAdapter<AppRole>>(),
                s.GetService<IMapper>()));

            services.AddScoped<IAppFrameworkRoleService>(s => new AppRoleService(
                s.GetService<IAppRoleDbService>(),
                s.GetService<IPermissionDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<IAuthDbService>(s => new AuthDbService(
                s.GetService<AppUserContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IReservedNameService>(s => new ReservedNameService(
                s.GetService<IReservedNameDbService>(),
                s.GetService<IMapper>()));

            var cultures = config["Cultures"].Split(',');

            services.AddScoped<ILegalDocumentDbService>(s => new LegalDocumentDbService(
                s.GetService<AppUserContext>(),
                cultures,
                s.GetService<IMapper>()));

            services.AddScoped<ILegalDocumentService>(s => new LegalDocumentService(
                s.GetService<ILegalDocumentDbService>(),
                s.GetService<IMapper>()));
        }
    }
}
