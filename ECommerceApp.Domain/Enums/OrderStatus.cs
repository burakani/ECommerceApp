namespace ECommerceApp.Domain.Enums
{
    /// <summary>
    /// Order Status
    /// </summary>
    public enum OrderStatus : byte
    {
        /// <summary>
        /// Pre-order
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Completed
        /// </summary>
        Completed = 1,

        /// <summary>
        /// Failed
        /// </summary>
        Failed = 2,

        /// <summary>
        /// Canceled
        /// </summary>
        Canceled = 3
    }
}
