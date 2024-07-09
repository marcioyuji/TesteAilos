using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Questao5.Infrastructure.Repositories
{
    public class MovimentoRepository : IMovimentoRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        public MovimentoRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<Movimento> SalvarMovimento(Movimento movimento)
        {
            using IDbConnection connection = new SqliteConnection(_connectionString);
            string sql = @"INSERT INTO MOVIMENTO (IdMovimento,IDCONTACORRENTE, DATAMOVIMENTO, TIPOMOVIMENTO, VALOR) VALUES (@IdMovimento,@IdContaCorrente,@DataMovimento,@TipoMovimento,@Valor);";

            await connection.ExecuteScalarAsync(sql, new
            {
                movimento.IdMovimento,
                movimento.IdContaCorrente,
                movimento.DataMovimento,
                movimento.TipoMovimento,
                movimento.Valor,
            });

            return movimento;
        }

        public async Task<decimal> ObterMovimentoPorIdConta(string idConta)
        {
            using IDbConnection connection = new SqliteConnection(_connectionString);

            string sql = "SELECT COALESCE(SUM(CASE WHEN TIPOMOVIMENTO = 'C' THEN valor " +
                                          "WHEN TIPOMOVIMENTO = 'D' THEN -valor "+
                                    "END),0) AS saldo "+
                         "FROM MOVIMENTO WHERE IDCONTACORRENTE =@idConta";
            
            return await connection.QueryFirstOrDefaultAsync<decimal>(sql, new { idConta });
        }
    }
}
