using ContactManager.Data.Dtos;
using ContactManager.Models;

namespace ContactManager.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(Guid id);
        Task<User> AddAsync(AddUserDto model);
        Task<IEnumerable<User>> AddRangeAsync(IEnumerable<AddUserDto> model);
        Task<User?> EditAsync(Guid id, UpdateUserDto model);
        Task<User?> DeleteAsync(Guid id);
    }
}
