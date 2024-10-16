namespace OpenHRCore.Domain.Enums
{
    /// <summary>
    /// Represents the gender identity of an individual.
    /// </summary>
    /// <remarks>
    /// This enum provides a standardized set of options for gender identification
    /// in the OpenHRCore system. It includes common gender identities and allows
    /// for inclusivity with options like NonBinary and Other.
    /// </remarks>
    public enum Gender
    {
        /// <summary>
        /// Represents individuals who identify as male.
        /// </summary>
        Male = 0,

        /// <summary>
        /// Represents individuals who identify as female.
        /// </summary>
        Female = 1,

        /// <summary>
        /// Represents individuals who identify as non-binary, neither exclusively male nor female.
        /// </summary>
        NonBinary = 2,

        /// <summary>
        /// Represents individuals who identify with a gender not listed in the other options.
        /// </summary>
        Other = 3,

        /// <summary>
        /// Represents cases where the gender is not known or not specified.
        /// </summary>
        Unknown = 4
    }
}
