using ContactManager.Data.Dtos;
using ContactManager.Models;

namespace ContactManager.Новая_папка
{
    public static class UserExtenstion
    { 
        public static UpdateUserDto ConvertToUpdateDto(this User user)
        {
            return new UpdateUserDto
            {
                Name = user.Name,
                Salary = user.Salary,
                DateOfBirth = user.DateOfBirth,
                IsMarried = user.Married,
                Phone = user.Phone
            };
        }
    }
}
