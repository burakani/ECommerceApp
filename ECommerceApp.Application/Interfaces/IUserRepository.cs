namespace ECommerceApp.Application.Interfaces
{
    using ECommerceApp.Domain.Entities;

    /// <summary>
    /// User Repository Interface
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// GetUserByUsername
        /// </summary>
        Task<User?> GetUserByUsername(string username);

        /// <summary>
        /// Add
        /// </summary>
        Task Add(User user);

        /// <summary>
        /// Save
        /// </summary>
        Task Save();
    }
}
