namespace mi_taller_contenedores.DB.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RolId { get; set; }
        public virtual Rol Rol { get; set; }
    }
}
