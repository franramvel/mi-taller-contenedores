namespace mi_taller_contenedores.DB.Model
{
    public class PagoPaypal
    {
        public int Id { get; set; }
        public int PaypalId { get; set; }
        public string Email { get; set; }
        public decimal Monto { get; set; }
    }
}
