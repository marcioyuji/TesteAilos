using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Domain.Enumerators;
using Questao5.Domain.util;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaCorrenteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContaCorrenteController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Movimentação de conta corrente
        /// </summary>
        /// <remarks>
        /// IdConta deve ser um identificador já cadastrado na contacorrente
        /// Valor deve ser maior que 0
        /// Tipo da conta deve ser 'C' (Crédito) ou 'D' (Débito)
        /// </remarks>
        /// <response code="200">Movimentação executada com sucesso retornando IdMovimento (Identificação do movimento)</response>
        /// <response code="400">Retorna BadRequest com tipo do erro e mensagem INVALID_ACCOUNT (Conta invalida),
        /// INACTIVE_ACCOUNT (Conta inativa), INVALID_VALUE (Valor invalido) e INVALID_TYPE </response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MovimentarConta(MovimentarContaRequest movimentarContarequest)
        {
            var resultMovimento = await _mediator.Send(movimentarContarequest);
            
            if (resultMovimento.Mensagem != ValidationError.VALID)
                return BadRequest(new { TipoFalha = resultMovimento.Mensagem.ToString(), Mensagem = Extentions.GetEnumDescription(resultMovimento.Mensagem) } );

            return Ok(new { IdMovimento = resultMovimento.Movimento.IdMovimento } );
        }

        /// <summary>
        /// Obter saldo da conta corrente
        /// </summary>
        /// <remarks>
        /// identificacaoConta deve ser um identificador já cadastrado na contacorrente
        /// </remarks>
        /// <response code="200">Dados da conta obtido com sucesso retornando Numero da conta, nome do titular, data da consulta e valor do saldo atual</response>
        /// <response code="400">Retorna BadRequest com tipo do erro e mensagem INVALID_ACCOUNT (Conta invalida),
        /// INACTIVE_ACCOUNT (Conta inativa)</response>
        [HttpGet("{identificacaoConta}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterSaldoConta(string identificacaoConta)
        {
            var resultSaldo = await _mediator.Send(new ObterSaldoContaRequest() { IdConta = identificacaoConta });

            if (resultSaldo.Mensagem != ValidationError.VALID)
                return BadRequest(new { TipoFalha = resultSaldo.Mensagem.ToString(), Mensagem = Extentions.GetEnumDescription(resultSaldo.Mensagem) });

            return Ok(new { DadosConta = resultSaldo });
        }
    }
}
