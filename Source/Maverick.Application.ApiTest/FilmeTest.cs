using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Maverick.Application.ApiTest.Services;
using Maverick.WebApi;
using Maverick.WebApi.Controllers;
using Maverick.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Otc.DomainBase.Exceptions;
using Xunit;

namespace Maverick.Application.ApiTest
{
    public class FilmeTest
    {

        private readonly Mock<FilmesServiceTest> filmesService = new Mock<FilmesServiceTest>();
        private readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private readonly Mock<HttpContext> contextMock = new Mock<HttpContext>();

        [Fact]
        public async void BuscarTesteSucesso()
        {

            //Arrange
            FilmesGet filmesGet = new FilmesGet
            {
                AnoLancamento = 2012,
                TermoPesquisa = "Teste"
            };

            //Act
            var filmesController = new FilmesController(
                filmesService.Object,
                mapper.Object);

            filmesController.ControllerContext.HttpContext = contextMock.Object;
            var actionResult = await filmesController.GetFilmesAsync(filmesGet);

            //Assert
            var viewResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<FilmesGetResult[]>(viewResult.Value);
        }
    }
}
