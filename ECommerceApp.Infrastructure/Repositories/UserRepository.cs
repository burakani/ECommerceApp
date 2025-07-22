namespace ECommerceApp.Infrastructure.Repositories
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;
    using ECommerceApp.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    /// <summary>
    /// User Repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        public async Task Add(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Save
        /// </summary>
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
