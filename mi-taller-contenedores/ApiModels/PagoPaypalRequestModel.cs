namespace mi_taller_contenedores.DB.Model
{
    public class PagoPaypalRequestModel
    {
        public int Id { get; set; }
        public int paypal_id { get; set; }
        public string email { get; set; }
        public decimal ammount_purchase { get; set; }
    }
}
