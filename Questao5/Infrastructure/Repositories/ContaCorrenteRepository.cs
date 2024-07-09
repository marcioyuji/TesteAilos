using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using System.Data;

namespace Questao5.Infrastructure.Repositories
{
    public class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        public ContaCorrenteRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<ContaCorrente> ObterContaCorrente(string idContaCorrente)
        {
            using var connection = new SqliteConnection(_connectionString);
            string sql = "SELECT IDCONTACORRENTE, NUMERO, NOME, ATIVO FROM CONTACORRENTE WHERE IDCONTACORRENTE=@idContaCorrente";
            
            return await connection.QueryFirstOrDefaultAsync<ContaCorrente>(sql, new { idContaCorrente });
        }
    }
}
