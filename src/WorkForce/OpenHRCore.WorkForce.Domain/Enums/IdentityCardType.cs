namespace OpenHRCore.WorkForce.Domain.Enums
{
    /// <summary>
    /// Represents the types of identity cards that can be used for identification.
    /// </summary>
    public enum IdentityCardType
    {
        /// <summary>
        /// National identification card issued by the government.
        /// </summary>
        NationalId,

        /// <summary>
        /// International travel document.
        /// </summary>
        Passport,

        /// <summary>
        /// License authorizing the holder to operate motor vehicles.
        /// </summary>
        DriversLicense,

        /// <summary>
        /// Document authorizing a foreign national to work in the country.
        /// </summary>
        WorkPermit,

        /// <summary>
        /// Identification card issued by the employer.
        /// </summary>
        CompanyId
    }

    /// <summary>
    /// Represents the current status of an identity card.
    /// </summary>
    public enum IdentityCardStatus
    {
        /// <summary>
        /// The identity card is currently valid and in use.
        /// </summary>
        Active,

        /// <summary>
        /// The identity card has passed its expiration date.
        /// </summary>
        Expired,

        /// <summary>
        /// The identity card has been officially canceled or withdrawn.
        /// </summary>
        Revoked
    }
}
