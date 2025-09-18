namespace waiter_app_miyako.Models
{
    public class Parcelamentos
    {
        public int numeroParcela { get; set; }
        public int condicoesPagamentoId { get; set; }

        public FormasPagamento? formaPagamento { get; set; }

        public int prazoDias { get; set; }
        public decimal porcentagemValor { get; set; }
    }
}
