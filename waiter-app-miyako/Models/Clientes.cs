namespace waiter_app_miyako.Models
{
    public class Clientes
    {
        public int id { get; set; }
        public string? cliente { get; set; }
        public string? apelido { get; set; }
        public string? genero { get; set; }
        public string? cpfCnpj { get; set; }
        public string? rg { get; set; }
        public DateTime? dataNascimento { get; set; }

        // FKs
        public int? cidadeId { get; set; }
        public Cidades? cidade { get; set; }

        public int? condicoesPagamentoId { get; set; }
        public CondicoesPagamento? condicaoPagamento { get; set; }

        public string? telefone { get; set; }
        public string? email { get; set; }
        public string? tipo { get; set; }
        public string? endereco { get; set; }
        public int? numeroEndereco { get; set; }
        public string? bairro { get; set; }
        public string? complemento { get; set; }
        public string? cep { get; set; }
        public string? anotacao { get; set; }
        public string? pratoPreferencial { get; set; }
        public bool? ativo { get; set; }
        public DateTime? dataCadastro { get; set; }
        public DateTime? dataAlteracao { get; set; }
    }
}
