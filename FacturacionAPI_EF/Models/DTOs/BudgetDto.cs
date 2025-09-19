namespace FacturacionAPI_EF.Models.DTOs
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public int FormaPago { get; set; }
        public DateTime Fecha { get; set; }
        public bool EstaActiva { get; set; }

        public List<BudgetDetailDto> DetallesFacturas { get; set; }
    }
}
