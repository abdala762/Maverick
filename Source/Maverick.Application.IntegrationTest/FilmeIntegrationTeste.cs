using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.Application.IntegrationTest.Services;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Xunit;

namespace Maverick.Application.IntegrationTest
{
    public class FilmeIntegrationTeste : IntegrationBaseTest
    {
        public FilmeIntegrationTeste(CustomWebApplicationFactory factory) : base(factory)
        {
            Client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddScoped<IFilmesService, FilmesServiceTests>();
                });
            })
            .CreateClient();
        }



        [Fact]
        private async Task BuscarFilmesAsync()
        {

            var response = await Client.GetAsync($"/v1/Filmes?TermoPesquisa=a&AnoLancamento=2012")
                                      .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            
            IEnumerable<Filme> filmes = JsonConvert.DeserializeObject<IEnumerable<Filme>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(filmes);
        }

        [Fact]
        private async Task InserirFilmeAsync()
        {
            Filme filme = new Filme()
            {
                Nome = "Teste",
                Descricao = "TEste"
            };

            var jsonContent = JsonConvert.SerializeObject(filme);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/v1/Filmes", contentString)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            IEnumerable<Filme> filmes = JsonConvert.DeserializeObject<IEnumerable<Filme>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(filmes);
        }

    }
}