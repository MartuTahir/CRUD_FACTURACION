namespace FacturacionAPI_EF.Models.DTOs
{
    public class BudgetDetailDto
    {
        public int IdDetalle { get; set; }
        public int IdArticulo { get; set; }
        public int Cantidad { get; set; }
        public int IdFactura { get;  set; }
    }
}
