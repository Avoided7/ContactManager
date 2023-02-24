using ContactManager.Data.Dtos;
using CsvHelper;
using System.Globalization;

namespace ContactManager.Services.Interfaces
{
    public class CSVFileReader<T> : ICSVFileReader<T>
    {
        public IEnumerable<T> ReadFromFileAll(IFormFile file)
        {
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<T>().ToList();
                    return records;
                }
            }
        }
    }
}
