namespace ECommerceApp.Domain.Entities
{
    using ECommerceApp.Domain.Enums;

    /// <summary>
    /// Balance Entity
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Total Balance
        /// </summary>
        public decimal TotalBalance { get; set; }

        /// <summary>
        /// Available Balance
        /// </summary>
        public decimal AvailableBalance { get; set; }

        /// <summary>
        /// Blocked Balance
        /// </summary>
        public decimal BlockedBalance { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Created At
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}