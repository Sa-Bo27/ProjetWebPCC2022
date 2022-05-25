using System.Reflection;
using Application.Auth;
using Application.Common.Behaviours;
using Application.Common.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddDependencyAplication(this IServiceCollection services,  IConfiguration configuration){
            
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //Add Auth0 service
            
            var domain = configuration["Auth0:Domain"];
            services.AddAuthentication(options => 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = domain;
                options.Audience = configuration["Auth0:Audience"];
            });
            
            services.AddAuthorization(options =>
            {
                options.AddPolicy("read:movieDom", policy => policy.Requirements.Add(new HasScopeRequirement("read:movieDom", domain)));
                options.AddPolicy("write:movieDto", policy => policy.Requirements.Add(new HasScopeRequirement("write:movieDto", domain)));
                options.AddPolicy("read:user", policy => policy.Requirements.Add(new HasScopeRequirement("read:user", domain)));
            });

            services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
        }
    }
}