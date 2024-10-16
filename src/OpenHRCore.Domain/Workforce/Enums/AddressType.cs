namespace OpenHRCore.Domain.Workforce.Enums
{
    /// <summary>
    /// Represents different types of addresses an employee may have.
    /// </summary>
    /// <remarks>
    /// This enum is used to categorize addresses in the OpenHRCore system.
    /// It allows for clear distinction between various address types associated with an employee.
    /// </remarks>
    public enum AddressType
    {
        /// <summary>
        /// Represents a residential address where the employee lives.
        /// </summary>
        Residential = 0,

        /// <summary>
        /// Represents a work or business address associated with the employee's place of employment.
        /// </summary>
        Business = 1,

        /// <summary>
        /// Represents a mailing address used for correspondence with the employee.
        /// </summary>
        Mailing = 2,

        /// <summary>
        /// Represents a temporary address for short-term stays or interim periods.
        /// </summary>
        Temporary = 3,

        /// <summary>
        /// Represents a permanent address for long-term residence or official records.
        /// </summary>
        Permanent = 4
    }
}
