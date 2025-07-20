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
        public Guid Id { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}