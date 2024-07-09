using Questao5.Domain.Entities;

namespace Questao5.Domain.Interfaces
{
    public interface IidEmpotenciaRepository
    {
        Task<IdEmpotencia> SalvarIdEmpotencia(IdEmpotencia movimento);
        Task<IdEmpotencia> ObterIdEmpotencia(string chaveIdEmpotencia);
    }
}
