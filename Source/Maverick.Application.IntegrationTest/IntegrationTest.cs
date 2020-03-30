using System;
using System.Net.Http;
using System.Threading.Tasks;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.WebApi;
using Newtonsoft.Json;
using Xunit;


namespace Maverick.Application.IntegrationTest
{
    public class IntegrationTest : IClassFixture<TestFixture<StartupTest>>
    {
        private HttpClient Client;

        public IntegrationTest(TestFixture<StartupTest> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestGetStockItemsAsync()
        {
            // Arrange
            var request = "/v1/Filmes?TermoPesquisa=a&AnoLancamento=2012";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
