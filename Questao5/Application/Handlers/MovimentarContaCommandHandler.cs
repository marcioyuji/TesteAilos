using MediatR;
using Newtonsoft.Json;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Domain.Interfaces;

namespace Questao5.Application.Handlers
{
    public class MovimentarContaCommandHandler : IRequestHandler<MovimentarContaRequest, MovimentarContaResponse>
    {
        private readonly IMovimentoRepository _movimentoRepository;
        private readonly IContaCorrenteRepository _contaCorrenteRepository;
        private readonly IidEmpotenciaRepository _idEmpotenciaRepository;

        public MovimentarContaCommandHandler(IMovimentoRepository movimentoRepository,
                                              IContaCorrenteRepository contaCorrenteRepository,
                                              IidEmpotenciaRepository idEmpotenciaRepository)
        {
            _movimentoRepository = movimentoRepository;
            _contaCorrenteRepository = contaCorrenteRepository;
            _idEmpotenciaRepository = idEmpotenciaRepository;
        }

        public async Task<MovimentarContaResponse> Handle(MovimentarContaRequest movimentorequest, CancellationToken cancellationToken)
        {
            var idEmpotente = await _idEmpotenciaRepository.ObterIdEmpotencia(movimentorequest.IdRequisicao);

            if (idEmpotente != null)
            {
                return JsonConvert.DeserializeObject<MovimentarContaResponse>(idEmpotente.Resultado);
            }

            var contaCorrente = await _contaCorrenteRepository.ObterContaCorrente(movimentorequest.IdConta);

            if (contaCorrente == null)
                return new MovimentarContaResponse() { Mensagem = ValidationError.INVALID_ACCOUNT };

            if (contaCorrente.Ativo == 0)
                return new MovimentarContaResponse() { Mensagem = ValidationError.INACTIVE_ACCOUNT };

            if (movimentorequest.Valor < 0)
                return new MovimentarContaResponse() { Mensagem = ValidationError.INVALID_VALUE };

            if (!Enum.TryParse(typeof(TipoMovimento), movimentorequest.TipoConta.ToUpper(), true, out var result))
            {
                return new MovimentarContaResponse() { Mensagem = ValidationError.INVALID_TYPE };
            }

            Movimento movimento = new Movimento()
            {
                IdMovimento = Guid.NewGuid().ToString(),
                IdContaCorrente = movimentorequest.IdConta,
                TipoMovimento = movimentorequest.TipoConta.ToUpper(),
                Valor = movimentorequest.Valor,
                DataMovimento = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };

            var movimentarContaResponse = new MovimentarContaResponse()
            {
                Movimento = await _movimentoRepository.SalvarMovimento(movimento),
                Mensagem = ValidationError.VALID
            };

            await _idEmpotenciaRepository.SalvarIdEmpotencia(new IdEmpotencia()
            {
              ChaveIdEmpotencia = Guid.NewGuid().ToString(),
              Requisicao = JsonConvert.SerializeObject(movimentorequest),
              Resultado = JsonConvert.SerializeObject(movimentarContaResponse)
            });

            return movimentarContaResponse;
        }
    }
}
