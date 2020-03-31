using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.Domain.Models;
using Newtonsoft.Json;
using Xunit;

namespace Maverick.Application.IntegrationTest
{
    public class FilmeIntegrationTeste : IntegrationBaseTest
    {
        public FilmeIntegrationTeste(CustomWebApplicationFactory factory) : base(factory)
        {
        }

        [Fact]
        private async Task BuscarFilmes()
        {

            var result = await Client.GetStringAsync($"/v1/Filmes?TermoPesquisa=a&AnoLancamento=2012")
                                      .ConfigureAwait(false);

            Assert.NotNull(result);
        }

        [Fact]
        private async Task InserirFilme()
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
        }

    }
}