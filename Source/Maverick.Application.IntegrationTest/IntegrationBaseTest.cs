

using System.Net.Http;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.Application.IntegrationTest.Services;
using Maverick.Domain.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Maverick.Application.IntegrationTest
{
    public class IntegrationBaseTest: IClassFixture<CustomWebApplicationFactory>
    {

        private readonly CustomWebApplicationFactory _factory;
        public HttpClient Client { get; }

        public IntegrationBaseTest(CustomWebApplicationFactory factory)
        {

            InMemoryDatabase.CreateDatabase();
            this._factory = factory;
            Client = this._factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IFilmesService, FilmesServiceTests>();
                });
            })
            .CreateClient();
        }

    }
}
