using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SavaDev.Infrastructure.Jwt;

namespace SavaDev.Users.Front
{
    public static class JwtModule
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtOptions = new JwtAppOptions();
            config.GetSection("Jwt").Bind(jwtOptions);
            services.AddSingleton<JwtAppOptions>(jwtOptions);

            services.AddScoped<JwtGenerator>(s => new JwtGenerator(jwtOptions));

            services.AddSingleton<CustomJwtSecurityTokenHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<JwtAppOptions, CustomJwtSecurityTokenHandler>((options, appOptions, tokenHandler) => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // укзывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // строка, представляющая издателя
                        ValidIssuer = appOptions.Issuer,

                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // установка потребителя токена
                        ValidAudience = appOptions.Audience,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,

                        // установка ключа безопасности
                        IssuerSigningKey = appOptions.GetSymmetricSecurityKey(),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                    };

                    options.SecurityTokenValidators.Clear();
                    //below line adds the custom validator class
                    options.SecurityTokenValidators.Add(tokenHandler);
                });
        }
    }
}
