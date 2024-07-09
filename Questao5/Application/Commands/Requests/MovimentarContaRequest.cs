using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class MovimentarContaRequest : IRequest<MovimentarContaResponse>
    {
        public string IdRequisicao { get; set; }
        public string IdConta { get; set; }
        public decimal Valor { get; set; }
        public string TipoConta { get; set; }
    }
}
