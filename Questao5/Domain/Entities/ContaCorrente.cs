using System.ComponentModel.DataAnnotations.Schema;

namespace Questao5.Domain.Entities
{
    public class ContaCorrente
    {
        [Column("idcontacorrente")]
        public string Id { get; set; }
        [Column("numero")]
        public int Numero { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("ativo")]
        public int Ativo { get; set; }
    }
}
