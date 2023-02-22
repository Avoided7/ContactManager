using ContactManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Data
{
    public class ContactManagerContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public ContactManagerContext(DbContextOptions<ContactManagerContext> options) : base(options) { }
    }
}
