using ContactManager.Data;
using ContactManager.Data.Dtos;
using ContactManager.Models;
using ContactManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services
{
    public class UserService : IUserService
    {
        private readonly ContactManagerContext _dbContext;
        public UserService(ContactManagerContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> AddUserAsync(AddUserDto model)
        {
            var newUser = new User
            {
                Name = model.Name,
                Salary = model.Salary,
                DateOfBirth = model.DateOfBirth,
                Married = model.Married,
                Phone = model.Phone
            };
            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            return newUser;
        }

        public async Task<User?> DeleteUserAsync(Guid id)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(user => user.Id == id);
            if(user != null)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync();
            }

            return user;
        }

        public async Task<User?> EditUserAsync(Guid id, UpdateUserDto model)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(user => user.Id == id);

            if(user != null)
            {
                user.Name = model.Name;
                user.Phone = model.Phone;
                user.DateOfBirth = model.DateOfBirth;
                user.Salary = model.Salary;
                user.Married = model.IsMarried;

                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Id == id);
        }
    }
}
