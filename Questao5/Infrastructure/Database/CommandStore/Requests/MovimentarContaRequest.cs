namespace Questao5.Infrastructure.Database.CommandStore.Requests
{
    public class MovimentarContaRequest
    {
        public int IdRequisicao { get; set; }
        public string IdConta { get; set; }
        public decimal Valor { get; set; }
        public int TipoConta { get; set; }
    }
}
