using System;
using Maverick.Application.IntegrationTest.Services;
using Maverick.Domain.Services;
using Maverick.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;



namespace Maverick.Application.IntegrationTest.Configuracoes
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                
            }).ConfigureServices(services =>
            {

                services.AddScoped<IFilmesService, FilmesServiceTests>();

            });
        }
    }
}
