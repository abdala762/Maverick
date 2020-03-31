using Maverick.Application.IntegrationTest.Configuracoes;

namespace Maverick.Application.IntegrationTest.Services
{
    internal class BaseService
    {
        public BaseService()
        {
            InMemoryDatabase.CreateDatabase();
        }
    }
}
