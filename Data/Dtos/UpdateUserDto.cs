using System.ComponentModel.DataAnnotations;

namespace ContactManager.Data.Dtos
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public bool IsMarried { get; set; }
        [Required]
        public string Phone { get; set; } = string.Empty;
        [Required, Range(100, 100000000)]
        public decimal Salary { get; set; }
    }
}