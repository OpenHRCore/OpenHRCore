namespace OpenHRCore.WorkForce.Domain.Enums
{
    /// <summary>
    /// Represents the type of employment for a work experience.
    /// </summary>
    /// <remarks>
    /// This enum is used to categorize different employment types in the OpenHRCore system.
    /// It provides a standardized set of options for classifying work experiences.
    /// </remarks>
    public enum EmploymentType
    {
        /// <summary>
        /// Full-time employment, typically involving a standard work week (e.g., 40 hours).
        /// </summary>
        FullTime = 0,

        /// <summary>
        /// Part-time employment, usually involving fewer hours than a full-time position.
        /// </summary>
        PartTime = 1,

        /// <summary>
        /// Contract-based employment with a defined term or project scope.
        /// </summary>
        Contract = 2,

        /// <summary>
        /// Temporary employment for a limited duration.
        /// </summary>
        Temporary = 3,

        /// <summary>
        /// Internship, often for students or recent graduates to gain work experience.
        /// </summary>
        Internship = 4,

        /// <summary>
        /// Freelance work, typically involving self-employment and multiple clients.
        /// </summary>
        Freelance = 5,

        /// <summary>
        /// Seasonal employment, often recurring during specific times of the year.
        /// </summary>
        Seasonal = 6,

        /// <summary>
        /// Volunteer work, unpaid and often for charitable or community organizations.
        /// </summary>
        Volunteer = 7,

        /// <summary>
        /// Other types of employment not covered by the above categories.
        /// </summary>
        Other = 8
    }
}
