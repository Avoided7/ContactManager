namespace ContactManager.Services.Interfaces
{
    public interface ICSVFileReader <T>
    {
        IEnumerable<T> ReadFromFileAll(IFormFile file);
    }
}
