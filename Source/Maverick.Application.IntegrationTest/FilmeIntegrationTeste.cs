using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.Application.IntegrationTest.Services;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Maverick.WebApi.Controllers;
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
        [Trait(nameof(FilmesController.GetFilmesAsync), "Sucesso")]
        private async Task BuscarFilmesAsyncSucesso()
        {

            var response = await Client.GetAsync($"/v1/Filmes?TermoPesquisa=a&AnoLancamento=2012")
                                      .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            
            IEnumerable<Filme> filmes = JsonConvert.DeserializeObject<IEnumerable<Filme>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(filmes);
            filmes.Should().BeEmpty();
        }


        [Fact]
        [Trait(nameof(FilmesController.GetFilmesAsync), "Erro")]
        private async Task BuscarFilmesAsyncErro()
        {

            var response = await Client.GetAsync($"/v1/Filmes")
                                      .ConfigureAwait(false);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        [Trait(nameof(FilmesController.InserirFilmesAsync), "Sucesso")]
        private async Task InserirFilmeAsync()
        {
            Filme filme = new Filme()
            {
                Id = 1,
                Nome = "Teste",
                Descricao = "TEste",
                DataLancamento = new DateTimeOffset(1, 1,1,0,0,0,new TimeSpan(0, 0, 0))
            };

            var jsonContent = JsonConvert.SerializeObject(filme);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/v1/Filmes", contentString)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            IEnumerable<Filme> filmes = JsonConvert.DeserializeObject<IEnumerable<Filme>>(await response.Content.ReadAsStringAsync());
            Assert.NotNull(filmes);

            filmes.Should().HaveCount(1);
            filmes.First().Should().BeEquivalentTo(filme);
        }

        [Fact]
        [Trait(nameof(FilmesController.InserirFilmesAsync), "Erro")]
        private async Task InserirFilmeAsyncErro()
        {
            Filme filme =null;

            var jsonContent = JsonConvert.SerializeObject(filme);
            var contentString = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync("/v1/Filmes", contentString)
                .ConfigureAwait(false);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

    }
}