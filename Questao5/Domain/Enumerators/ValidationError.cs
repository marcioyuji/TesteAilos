using System.ComponentModel;

namespace Questao5.Domain.Enumerators
{
    public enum ValidationError
    {
        [Description("Conta Invalida")]
        INVALID_ACCOUNT,
        [Description("Conta Inativa")]
        INACTIVE_ACCOUNT,
        [Description("Valor Invalido")]
        INVALID_VALUE,
        [Description("Tipo Invalido")]
        INVALID_TYPE,
        [Description("Validao")]
        VALID
    }
}
