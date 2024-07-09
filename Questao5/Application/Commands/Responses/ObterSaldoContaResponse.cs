using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class ObterSaldoContaResponse
    {
        public ValidationError Mensagem { get; set; }
        public int NumeroConta { get; set; }
        public string NomeTitular { get; set; }
        public string DataConsulta { get; set; }
        public decimal Saldo { get; set; }
    }
}
