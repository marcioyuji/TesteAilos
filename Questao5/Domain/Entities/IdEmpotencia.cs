using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public class IdEmpotencia
    {
        [Column("chave_idmpotencia")]
        public string ChaveIdEmpotencia { get; set; }
        [Column("requisicao")]
        public string Requisicao { get; set; }
        [Column("resultado")]
        public string Resultado { get; set; }
    }
}
