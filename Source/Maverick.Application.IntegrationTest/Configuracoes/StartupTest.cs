using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Maverick.Application;
using Maverick.TmdbAdapter;
using Maverick.WebApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Otc.AspNetCore.ApiBoot;
using Otc.Extensions.Configuration;

namespace Maverick.Application.IntegrationTest.Configuracoes
{
    public class StartupTest : ApiBootStartup
    {
        protected override ApiMetadata ApiMetadata => new ApiMetadata()
        {
            Name = "Maverick",
            Description = "{{webAPIDescription}}",
            DefaultApiVersion = "1.0"
        };

        public StartupTest(IConfiguration configuration)
            : base(configuration)
        {

        }

        /// <summary>
        /// Registra os servicos especificos da API.
        /// </summary>
        /// <param name="services"></param>
        [ExcludeFromCodeCoverage]
        protected override void ConfigureApiServices(
            IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(TmdbMapperProfile),
                typeof(WebApiMapperProfile));

            services.AddTmdbAdapter(
                Configuration.SafeGet<TmdbAdapterConfiguration>());

            services.AddApplication
                (Configuration.SafeGet<ApplicationConfiguration>());
        }
    }
}
