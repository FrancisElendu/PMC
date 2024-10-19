using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PMC.Application.Mapper;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace PMC.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

        }
    }
}
