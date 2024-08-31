using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents the bank information associated with an employee.
    /// </summary>
    public class EmployeeBankInformation : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this bank information.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the bank.
        /// </summary>
        public required string BankName { get; set; }

        /// <summary>
        /// Gets or sets the bank account number.
        /// </summary>
        public required string AccountNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the account holder.
        /// </summary>
        public required string AccountHolderName { get; set; }

        /// <summary>
        /// Gets or sets the branch name where the account is held.
        /// </summary>
        public string? BranchName { get; set; }

        /// <summary>
        /// Gets or sets the SWIFT code of the bank.
        /// </summary>
        public string? SWIFTCode { get; set; }

        /// <summary>
        /// Gets or sets the IFSC code of the bank.
        /// </summary>
        public string? IFSCCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this bank account is the default account for the employee.
        /// </summary>
        public bool IsDefault { get; set; } = false;

        /// <summary>
        /// Gets or sets any additional notes related to the bank information.
        /// </summary>
        public string? Notes { get; set; }
    }
}
