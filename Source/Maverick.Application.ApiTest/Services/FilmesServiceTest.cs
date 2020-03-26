using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maverick.Domain.Adapters;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Maverick.Application.ApiTest.Services
{
    public class FilmesServiceTest : IFilmesService
    {
        private readonly IFilmesService filmesService;
        private readonly Mock<ITmdbAdapter> tmdbAdapterMock;

        public FilmesServiceTest()
        {
            tmdbAdapterMock = new Mock<ITmdbAdapter>();
            filmesService = new FilmesService(
                tmdbAdapterMock.Object,
                new ApplicationConfiguration(),
                new LoggerFactory());
        }

        public async Task<IEnumerable<Filme>> ObterFilmesAsync(Pesquisa pesquisa)
        {
            // Objeto que sera utilizado para retorno do Mock
            var expected = new List<Filme>()
                {
                    new Filme()
                    {
                        Id = 10447,
                        Descricao = "descricao_teste",
                        Nome = "nome_teste"
                    }
                };

            tmdbAdapterMock
                .Setup(m => m.GetFilmesAsync(It.IsAny<Pesquisa>(), "pt-BR"))
                .ReturnsAsync(expected);

            var filmes = await filmesService.ObterFilmesAsync(new Pesquisa()
            {
                TermoPesquisa = "teste"
            });
            return filmes;
        }
    }
}
