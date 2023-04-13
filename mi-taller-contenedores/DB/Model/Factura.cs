namespace mi_taller_contenedores.DB.Model
{
    //Esta clase, es un mapeo de la tabla que tendrémos en nuestro motor de SQL
    public class Factura
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string UUID { get; set; }
        public int Pasajeros { get; set; }
        public decimal  MontoPorPasajero { get; set; }
        public string  PathFileFactura { get; set; }
    }
}
