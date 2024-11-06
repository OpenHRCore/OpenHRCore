namespace OpenHRCore.Domain.EmployeeModule.Entities
{
    /// <summary>
    /// Represents the bank information associated with an employee.
    /// This class encapsulates all relevant banking details for an employee,
    /// including account information, bank identifiers, and additional metadata.
    /// </summary>
    public class EmployeeBankInformation : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this bank information.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship between the bank information and a specific employee.
        /// It should correspond to the Id of an existing EmployeeInfo entity.
        /// </remarks>
        public required Guid EmployeeId { get; set; }

        public required virtual EmployeeInfo Employee { get; set; }

        /// <summary>
        /// Gets or sets the official name of the bank where the account is held.
        /// </summary>
        public required string BankName { get; set; }

        /// <summary>
        /// Gets or sets the unique account number associated with the bank account.
        /// </summary>
        /// <remarks>
        /// This should be the full account number as provided by the bank.
        /// Consider implementing validation to ensure the format is correct based on regional standards.
        /// </remarks>
        public required string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the full name of the account holder as it appears on the bank account.
        /// </summary>
        /// <remarks>
        /// This may differ from the employee's name in some cases, such as joint accounts.
        /// </remarks>
        public required string AccountHolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the specific branch where the account is held, if applicable.
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// Gets or sets the SWIFT (Society for Worldwide Interbank Financial Telecommunication) code of the bank.
        /// </summary>
        /// <remarks>
        /// This is also known as the BIC (Bank Identifier Code) and is used for international transfers.
        /// Consider implementing validation for the SWIFT code format (8 or 11 characters).
        /// </remarks>
        public string? SwiftCode { get; set; }

        /// <summary>
        /// Gets or sets the IFSC (Indian Financial System Code) of the bank branch.
        /// </summary>
        /// <remarks>
        /// This is specific to Indian banks and is used for electronic fund transfers.
        /// Consider implementing validation for the IFSC code format (11 characters).
        /// </remarks>
        public string? IfscCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this bank account is the primary account for the employee.
        /// </summary>
        /// <remarks>
        /// Only one account per employee should be set as the default.
        /// Consider implementing logic to ensure this constraint is maintained.
        /// </remarks>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Gets or sets any additional notes or comments related to the bank information.
        /// </summary>
        /// <remarks>
        /// This field can be used to store any relevant information that doesn't fit into other properties.
        /// </remarks>
        public string? Notes { get; set; }
    }
}
