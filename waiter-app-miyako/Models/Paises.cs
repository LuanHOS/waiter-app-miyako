namespace waiter_app_miyako.Models
{
    public class Paises
    {
        public int id { get; set; }
        public string? pais { get; set; }
        public string? sigla { get; set; }
        public string? moeda { get; set; }
        public string? ddi { get; set; }
        public bool? ativo { get; set; }
        public DateTime? dataCadastro { get; set; }
        public DateTime? dataAlteracao { get; set; }
    }
}

