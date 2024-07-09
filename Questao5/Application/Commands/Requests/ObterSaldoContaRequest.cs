using MediatR;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Commands.Requests
{
    public class ObterSaldoContaRequest : IRequest<ObterSaldoContaResponse>
    {
        public string IdConta { get; set; }
    }
}
