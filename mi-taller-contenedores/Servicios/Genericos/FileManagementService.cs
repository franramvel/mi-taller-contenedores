namespace mi_taller_contenedores.Servicios.Genericos
{
    public class FileManagementService : IFileManagementService
    {
        public string SaveFile(string savePath, string base64)
        {
            //Simulacion de guardado
            return "C:\\temp\\file.txt";
        }

        public string SaveFile(string savePath, Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public string SaveFile(string savePath, IFormFile fileStream)
        {
            throw new NotImplementedException();
        }
    }
}
