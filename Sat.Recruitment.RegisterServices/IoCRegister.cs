using Microsoft.Extensions.DependencyInjection;
using Sat.Recruitment.Business.Abtractions;
using Sat.Recruitment.Business.Implementations;
using Sat.Recruitment.Data;
using Sat.Recruitment.Data.Abstractions;
using Sat.Recruitment.Data.Implementations;
using System;

namespace Sat.Recruitment.RegisterServices
{
    public static class IoCRegister
    {
        public static IServiceCollection AddRegistration(this IServiceCollection services)
        {
            AddRegisterServices(services);

            return services;
        }

        private static IServiceCollection AddRegisterServices(IServiceCollection services)
        {
            #region instances
            
            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddScoped<IValidations, Validations>();
            services.AddScoped<IEmail, Email>();
            services.AddScoped<IUserValidations, UserValidations>();
            services.AddScoped<IDataFromFile, DataFromFile>();

            #endregion
            return services;
        }
    }
}
