using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<Movimento> SalvarMovimento(Movimento movimento);
        Task<decimal> ObterMovimentoPorIdConta(string idConta);
    }
}
