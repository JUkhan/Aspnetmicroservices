using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR;

namespace Ordering.Application
{
    public static class ApplicatioServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidation(new[] { typeof(ApplicatioServiceRegistration).Assembly });
            services.AddMediatR(typeof(ApplicatioServiceRegistration).Assembly);
            return services;
        }
    }
}
