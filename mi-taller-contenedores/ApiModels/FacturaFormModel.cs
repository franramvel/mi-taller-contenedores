namespace mi_taller_contenedores.ApiModels
{
    //Esta clase, es un mapeo de la tabla que tendrémos en nuestro motor de SQL
    public class FacturaFormModel
    {
    
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public int Pasajeros { get; set; }
        public decimal  MontoPorPasajero { get; set; }
        public string? UUID { get; set; }
    }
}
