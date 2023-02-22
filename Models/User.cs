using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public bool Married { get; set; }
        public string Phone { get; set; } = string.Empty;
        public decimal Salary { get; set; }
    }
}
