using Dapper;
using Microsoft.Data.Sqlite;
using Questao5.Domain.Entities;
using Questao5.Domain.Interfaces;

namespace Questao5.Infrastructure.Repositories
{
    public class IdEmpotenciaRepository : IidEmpotenciaRepository
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;
        public IdEmpotenciaRepository(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public async Task<IdEmpotencia> ObterIdEmpotencia(string chaveIdEmpotencia)
        {
            using var connection = new SqliteConnection(_connectionString);
            string sql = "SELECT CHAVE_IDEMPOTENCIA, REQUISICAO, RESULTADO FROM IDEMPOTENCIA WHERE CHAVE_IDEMPOTENCIA=@chaveIdEmpotencia";

            return await connection.QueryFirstOrDefaultAsync<IdEmpotencia>(sql, new { chaveIdEmpotencia });
        }

        public async Task<IdEmpotencia> SalvarIdEmpotencia(IdEmpotencia IdEmpotencia)
        {
            using var connection = new SqliteConnection(_connectionString);
            string sql = @"INSERT INTO IDEMPOTENCIA (chave_idempotencia, requisicao, resultado) VALUES (@ChaveIdEmpotencia, @Requisicao, @Resultado);";

            await connection.ExecuteScalarAsync(sql, new
            {
                IdEmpotencia.ChaveIdEmpotencia,
                IdEmpotencia.Requisicao,
                IdEmpotencia.Resultado
            });

            return IdEmpotencia;
        }
    }
}