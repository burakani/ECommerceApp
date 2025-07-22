namespace ECommerceApp.Infrastructure.Repositories
{
    using ECommerceApp.Application.Interfaces;
    using ECommerceApp.Domain.Entities;
    using ECommerceApp.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// User Balance Repository
    /// </summary>
    public class UserBalanceRepository : IUserBalanceRepository
    {
        private readonly AppDbContext _context;

        public UserBalanceRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Add
        /// </summary>
        public async Task Add(Balance balance)
        {
            await _context.Balances.AddAsync(balance);
        }

        /// <summary>
        /// Get user's available balance
        /// </summary>
        public async Task<decimal> GetUserAvailableBalance(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            try
            {
                var userBalance = await _context.Balances.Where(x => x.UserId == userId).FirstOrDefaultAsync();

                return userBalance?.AvailableBalance ?? 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

        /// <summary>
        /// Lock user's balance
        /// </summary>
        public async Task Lock(string userId, decimal amount)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            var userBalance = await _context.Balances.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (userBalance == null)
            {
                throw new InvalidOperationException($"User balance not found for user ID: {userId}");
            }

            if (userBalance.AvailableBalance < amount)
            {
                throw new InvalidOperationException("Insufficient balance to lock the specified amount.");
            }

            userBalance.BlockedBalance += amount;
            userBalance.AvailableBalance += amount;
            userBalance.TotalBalance = Math.Abs(userBalance.AvailableBalance) + Math.Abs(userBalance.BlockedBalance);
            userBalance.UpdatedAt = DateTime.UtcNow;            

            _context.Balances.Update(userBalance);

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Update user's balance
        /// </summary>
        public async Task Update(string userId, decimal amount)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentException("User ID cannot be null or empty.", nameof(userId));
            }

            var userBalance = await _context.Balances.Where(x => x.UserId == userId).FirstOrDefaultAsync();

            if (userBalance == null)
            {
                throw new InvalidOperationException($"User balance not found for user ID: {userId}");
            }

            userBalance.AvailableBalance += amount;
            userBalance.BlockedBalance += amount * -1;
            userBalance.TotalBalance = Math.Abs(userBalance.AvailableBalance) + Math.Abs(userBalance.BlockedBalance);
            userBalance.UpdatedAt = DateTime.UtcNow;

            _context.Balances.Update(userBalance);

            await _context.SaveChangesAsync();
        }
    }
}
