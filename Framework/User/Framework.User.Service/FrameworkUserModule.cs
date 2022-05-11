using AutoMapper;
using Framework.User.DataService.Contract.Interfaces;
using Framework.User.DataService.Entities;
using Framework.User.DataService.Services;
using Framework.User.Service.Contract.Interfaces;
using Framework.User.Service.Services;
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
    public static class FrameworkUserModule
    {
        public static void AddFrameworkUser(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentity<FrameworkUser, FrameworkRole>()
                .AddEntityFrameworkStores<FrameworkUserDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
            });

            services.AddScoped<IUserManagerAdapter<FrameworkUser>>(s => new UserManagerAdapter<FrameworkUser>(
                s.GetService<UserManager<FrameworkUser>>()));

            services.AddScoped<ISignInManagerAdapter>(s => new SignInManagerAdapter<FrameworkUser>(
                s.GetService<UserManager<FrameworkUser>>(),
                s.GetService<SignInManager<FrameworkUser>>()));

            services.AddScoped<IFrameworkUserDbService>(s => new FrameworkUserDbService(
                s.GetService<FrameworkUserDbContext>(),
                s.GetService<IUserManagerAdapter<FrameworkUser>>(),
                s.GetService<IMapper>()));

            services.AddScoped<IReservedNameDbService>(s => new ReservedNameDbService(
                s.GetService<FrameworkUserDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IFrameworkUserService>(s => new FrameworkUserService(
                s.GetService<IFrameworkUserDbService>(),
                s.GetService<ISignInManagerAdapter>(),
                s.GetService<IReservedNameDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<IPermissionDbService>(s => new PermissionDbService(s.GetService<FrameworkUserDbContext>(), s.GetService<IMapper>()));
            services.AddScoped<IPermissionService>(s => new PermissionService(
                s.GetService<IPermissionDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<IRoleManagerAdapter<FrameworkRole>>(s => new RoleManagerAdapter<FrameworkRole>(
                s.GetService<RoleManager<FrameworkRole>>()));

            services.AddScoped<IFrameworkRoleDbService>(s => new FrameworkRoleDbService(
                s.GetService<FrameworkUserDbContext>(),
                s.GetService<IRoleManagerAdapter<FrameworkRole>>(),
                s.GetService<IMapper>()));

            services.AddScoped<IFrameworkRoleService>(s => new FrameworkRoleService(
                s.GetService<IFrameworkRoleDbService>(),
                s.GetService<IPermissionDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<IAuthDbService>(s => new AuthDbService(
                s.GetService<FrameworkUserDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<IReservedNameService>(s => new ReservedNameService(
                s.GetService<IReservedNameDbService>(),
                s.GetService<IMapper>()));

            services.AddScoped<ILegalDocumentDbService>(s => new LegalDocumentDbService(
                s.GetService<FrameworkUserDbContext>(),
                s.GetService<IMapper>()));

            services.AddScoped<ILegalDocumentService>(s => new LegalDocumentService(
                s.GetService<ILegalDocumentDbService>(),
                s.GetService<IMapper>()));
        }
    }
}
