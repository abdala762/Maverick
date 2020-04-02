using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Maverick.Application.IntegrationTest.Configuracoes;
using Maverick.Domain.Models;
using Maverick.Domain.Services;
using Otc.Validations.Helpers;

namespace Maverick.Application.IntegrationTest.Services
{
    internal class FilmesServiceTests : IFilmesService 
    {
        private const string INSERT_SQL = "INSERT INTO Filme (Nome, Descricao) Values (@Nome,@Descricao);";
        private const string SELECT_SQL = "Select * FROM Filme";
        private const string SELECT_WITH_WHERE_SQL = "Select * FROM Filme WHERE Nome = @Nome AND DataLancamento=@DataLancamento";

        public async Task<IEnumerable<Filme>> InserirFilmeAsync(Filme filme)
        {

            ValidationHelper.ThrowValidationExceptionIfNotValid(filme);
            var conn = InMemoryDatabase.GetInMemoryOpenSqliteConnection();
            conn.Execute(INSERT_SQL, new { filme.Nome, filme.Descricao });
            return await conn.QueryAsync<Filme>(SELECT_SQL);
        }

        public async Task<IEnumerable<Filme>> ObterFilmesAsync(Pesquisa pesquisa)
        {

            ValidationHelper.ThrowValidationExceptionIfNotValid(pesquisa);
            var conn = InMemoryDatabase.GetInMemoryOpenSqliteConnection();

            return await conn.QueryAsync<Filme>(SELECT_WITH_WHERE_SQL,
                new { Nome = pesquisa.TermoPesquisa, DataLancamento = pesquisa.AnoLancamento });
        }
    }
}
