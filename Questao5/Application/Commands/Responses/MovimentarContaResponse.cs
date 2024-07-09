using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class MovimentarContaResponse
    {
        public ValidationError Mensagem { get; set; }
        public Movimento Movimento { get; set; }
    }
}
