using MediatR;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class ObterSaldoContaCommandHandler : IRequestHandler<ObterSaldoContaRequest, ObterSaldoContaResponse>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public ObterSaldoContaCommandHandler(IMovimentoRepository movimentoRepository,
                                              IContaCorrenteRepository contaCorrenteRepository)
        {
            _movimentoRepository = movimentoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        public async Task<ObterSaldoContaResponse> Handle(ObterSaldoContaRequest request, CancellationToken cancellationToken)
        {
            var contaCorrente = await _contaCorrenteRepository.ObterContaCorrente(request.IdConta);

            if (contaCorrente == null)
                return new ObterSaldoContaResponse() { Mensagem = ValidationError.INVALID_ACCOUNT };

            if (contaCorrente.Ativo == 0)
                return new ObterSaldoContaResponse() { Mensagem = ValidationError.INACTIVE_ACCOUNT };

            var valorSaldo = await _movimentoRepository.ObterMovimentoPorIdConta(request.IdConta);

            return new ObterSaldoContaResponse()
            {
                Mensagem = ValidationError.VALID,
                NomeTitular = contaCorrente.Nome,
                DataConsulta = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                NumeroConta = contaCorrente.Numero,
                Saldo = valorSaldo
            };
        }
    }
}
