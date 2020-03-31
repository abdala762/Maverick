using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.Domain.Models;
using Maverick.Domain.Services;

namespace Maverick.Application.IntegrationTest.Services
{
    internal class FilmesServiceTests : BaseService, IFilmesService 
    {
        public async Task<IEnumerable<Filme>> InserirFilmeAsync(Filme filme)
        {

            string sql = "INSERT INTO Filme (Nome, Descricao) Values (@Nome,@Descricao);";

            var conn = InMemoryDatabase.GetInMemoryOpenSqliteConnection();
            conn.Execute(sql, new { Nome = filme.Nome, Descricao = filme.Descricao });
            var filmes = conn.Query<Filme>("Select * FROM Filme").ToList();

            return filmes;
        }

        public async Task<IEnumerable<Filme>> ObterFilmesAsync(Pesquisa pesquisa)
        {

            var conn = InMemoryDatabase.GetInMemoryOpenSqliteConnection();


            var filmes = conn.Query<Filme>("Select * FROM Filme WHERE Nome = @Nome AND DataLancamento=@DataLancamento",
                new { Nome = pesquisa.TermoPesquisa, DataLancamento = pesquisa.AnoLancamento }).ToList();

            return filmes;
        }
    }
}
