namespace InventoryManagement.Infrastructure.Services
{
    public class FileLogger
    {
        private readonly string _filePath = "log.txt";

        public FileLogger() {}

        public void WriteToFile(string text)
        {
             File.AppendAllText(_filePath, $"[File] {text}\n");
        }
    }
}
