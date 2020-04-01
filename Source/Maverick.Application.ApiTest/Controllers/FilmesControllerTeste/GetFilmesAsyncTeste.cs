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
            var cenarioEsperado = new List<FilmesGetResult>
            {
                new FilmesGetResult
                {   DataLancamento = DateTime.Now,
                    Id = 10448,
                    Descricao = "descricao_test_2",
                    Nome = "nome_teste_2"
                },
                new FilmesGetResult
                {
                    DataLancamento = DateTime.Now,
                    Id = 10449,
                    Descricao = "descricao_test_12",
                    Nome = "nome_teste_3"
                },
                new FilmesGetResult
                {
                    DataLancamento = DateTime.Now,
                    Id = 10447,
                    Descricao = "descricao_teste",
                    Nome = "nome_teste"
                }
            };

            mapper.Setup(m => m.Map<IEnumerable<FilmesGetResult>>(It.IsAny<IEnumerable<Filme>>()))
              .Returns(cenarioEsperado);

            OkObjectResult resultadoRespostaOK = Assert.IsType<OkObjectResult>(
                await filmesController.GetFilmesAsync(
                    new FilmesGet
                    {
                        TermoPesquisa = "a",
                        AnoLancamento = 2012
                    }
                )
            );

            Assert.IsAssignableFrom<List<FilmesGetResult>>(resultadoRespostaOK.Value);
        }
    }

    public class FilmeModelTeste
    {
        public string Nome { get; set; }
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTimeOffset DataLancamento { get; set; }
    }
}