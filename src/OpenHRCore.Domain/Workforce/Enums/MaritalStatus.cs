namespace OpenHRCore.Domain.Workforce.Enums
{
    /// <summary>
    /// Represents the marital status of an individual.
    /// </summary>
    /// <remarks>
    /// This enum provides a standardized set of options for marital status
    /// in the OpenHRCore system. It includes common marital statuses and allows
    /// for cases where the status is unknown or not specified.
    /// </remarks>
    public enum MaritalStatus
    {
        /// <summary>
        /// Represents an individual who has never been married.
        /// </summary>
        Single = 0,

        /// <summary>
        /// Represents an individual who is currently married.
        /// </summary>
        Married = 1,

        /// <summary>
        /// Represents an individual who was previously married but is now legally divorced.
        /// </summary>
        Divorced = 2,

        /// <summary>
        /// Represents an individual whose spouse has died and who has not remarried.
        /// </summary>
        Widowed = 3,

        /// <summary>
        /// Represents cases where the marital status is not known or not specified.
        /// </summary>
        Unknown = 4,

        /// <summary>
        /// Represents an individual who is legally separated but not divorced.
        /// </summary>
        Separated = 5,

        /// <summary>
        /// Represents an individual in a domestic partnership or civil union.
        /// </summary>
        DomesticPartnership = 6
    }
}
