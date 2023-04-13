namespace mi_taller_contenedores.Servicios.Genericos
{
    public interface IFileManagementService
    {
        string SaveFile(string savePath, string base64);
        string SaveFile(string savePath, Stream fileStream);
        string SaveFile(string savePath, IFormFile fileStream);
    }
}