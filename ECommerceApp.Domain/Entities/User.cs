namespace ECommerceApp.Domain.Entities
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; } = default!;

        /// <summary>
        /// Password hash
        /// </summary>
        public byte[] PasswordHash { get; set; } = default!;

        /// <summary>
        /// Password salt
        /// </summary>
        public byte[] PasswordSalt { get; set; } = default!;

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}