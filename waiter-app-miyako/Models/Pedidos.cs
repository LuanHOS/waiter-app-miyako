using System.Text.Json.Serialization;

namespace waiter_app_miyako.Models
{
    public class Pedidos
    {
        [JsonPropertyName("mesa")]
        public int id { get; set; }

        // FKs (coluna no banco é MesaNumero)
        public int? mesaNumero { get; set; }
        public Mesas? mesa { get; set; }

        public int? clientes{ get; set; }

        public int? funcionarioId { get; set; }
        public Funcionarios? funcionario { get; set; }

        public int? vendaId { get; set; }
        public Vendas? venda { get; set; }

        public string? observacao { get; set; }

        public bool? finalizado { get; set; }

        public DateTime? dataAberturaPedido { get; set; }
        public DateTime? dataConclusaoPedido { get; set; }

        public bool? ativo { get; set; }
        public DateTime? dataCadastro { get; set; }
        public DateTime? dataAlteracao { get; set; }

        // Caso você queira retornar itens neste GET:
        public List<ItensPedidos>? itens { get; set; }
    }
}
