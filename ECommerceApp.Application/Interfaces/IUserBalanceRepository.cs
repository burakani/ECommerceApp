namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Domain.Entities;

    /// <summary>
    /// User Balance Service Interface
    /// </summary>
    public interface IUserBalanceRepository
    {
        /// <summary>
        /// Add user new balance
        /// </summary>
        Task Add(Balance balance);

        /// <summary>
        /// Get user available balance
        /// </summary>
        Task<decimal> GetUserAvailableBalance(string userId);

        /// <summary>
        /// Lock user balance
        /// </summary>
        Task Lock(string userId, decimal amount);

        /// <summary>
        /// Update user balance
        /// </summary>
        Task Update(string userId, decimal amount);
    }
}
