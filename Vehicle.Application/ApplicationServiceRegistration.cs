using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); //registering all AutoMapper profiles in the same assembly. This method will scan your entire assembly for any classes inheriting from the Profile class and automatically register them.
            services.AddMediatR(Assembly.GetExecutingAssembly());
            // Register FluentValidation with the assembly containing the validators
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("fa"),
                    new CultureInfo("en"),
                    new CultureInfo("fr")
                };
                options.DefaultRequestCulture = new RequestCulture("fa");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            return services;
        }
    }
}
