namespace Facturacion.Domain
{
    public class PayMethod
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}