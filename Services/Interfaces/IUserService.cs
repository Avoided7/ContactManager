using ContactManager.Data.Dtos;
using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddUserAsync(AddUserDto model);
        Task<User?> EditUserAsync(Guid id, UpdateUserDto model);
        Task<User?> DeleteUserAsync(Guid id);
    }
}
