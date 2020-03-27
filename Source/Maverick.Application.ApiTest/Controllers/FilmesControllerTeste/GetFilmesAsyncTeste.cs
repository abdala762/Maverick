using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Maverick.Domain.Exceptions;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Maverick.WebApi.Controllers;
using Maverick.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Otc.DomainBase.Exceptions;
using Xunit;

namespace Maverick.Application.ApiTest.Controllers.FilmesControllerTeste
{
    public class GetFilmesAsyncTeste
    {

        private readonly FilmesController filmesController;
        private readonly Mock<IFilmesService> filmeServiceMock;
        private readonly Mock<IMapper> mapper = new Mock<IMapper>();


        public GetFilmesAsyncTeste()
        {
            filmeServiceMock = new Mock<IFilmesService>();
            filmesController = new FilmesController(
                filmeServiceMock.Object,
                 mapper.Object);
        }

        [Fact]
        public async Task ObterFilmesAsync_Sucesso()
        {
            var expected = new List<FilmesGetResult>
                {
                    new FilmesGetResult
                    {
                        Id = 10448,
                        Descricao = "descricao_test_2",
                        Nome = "nome_teste_2"
                    },
                    new FilmesGetResult
                    {
                        Id = 10449,
                        Descricao = "descricao_test_12",
                        Nome = "nome_teste_3"
                    },
                    new FilmesGetResult
                    {
                        Id = 10447,
                        Descricao = "descricao_teste",
                        Nome = "nome_teste"
                    }
                };

            mapper.Setup(m => m.Map<IEnumerable<FilmesGetResult>>(It.IsAny<IEnumerable<Filme>>())).Returns(expected);

            var filmes = await filmesController.GetFilmesAsync(new FilmesGet
            {
                TermoPesquisa = "a",
                AnoLancamento = 2012
            });

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(filmes);
            //Assert garantindo que variavel filmes tem os campos e tipos de campos exatamente iguais a classe FilmeModelTeste
            Assert.IsAssignableFrom<List<FilmesGetResult>>(viewResult.Value);
            var listaFilmeRetornado = (List<FilmesGetResult>)viewResult.Value;
          
            var listaFilmeEsperado = new List<FilmeModelTeste>
            {
                new FilmeModelTeste
                {
                    Id = 10447,
                    Descricao = "descricao_teste",
                    Nome = "nome_teste"
                },
                new FilmeModelTeste
                {
                    Id = 10448,
                    Descricao = "descricao_test_2",
                    Nome = "nome_teste_2"
                },
                new FilmeModelTeste
                {
                    Id = 10449,
                    Descricao = "descricao_test_12",
                    Nome = "nome_teste_3"
                }
            };

            TesteUtil.VerificarListasClassesIguais(listaFilmeEsperado, listaFilmeRetornado);
        }
    }

    public class FilmeModelTeste
    {
        /// <summary>
        /// Nome do filme.
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Identificador do filme no The Moview Database.
        /// </summary>
        public long Id { get; set; }


        /// <summary>
        /// Descricao/Resumo do filme.
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Data de lancamento do filme.
        /// </summary>
        public DateTimeOffset DataLancamento { get; set; }
    }

}
