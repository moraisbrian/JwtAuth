namespace JwtAuth.Domain.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Identificacao { get; set; }
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
    }
}