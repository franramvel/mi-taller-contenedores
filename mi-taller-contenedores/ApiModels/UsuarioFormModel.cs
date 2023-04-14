namespace mi_taller_contenedores.DB.Model
{
    public class UsuarioFormModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public virtual RolFormModel Rol { get; set; }
    }
}