using OpenHRCore.SharedKernel.Domain.Entities;

namespace OpenHRCore.WorkForce.Domain.Entities
{
    /// <summary>
    /// Represents a document associated with an employee.
    /// </summary>
    public class EmployeeDocument : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the ID of the employee associated with this document.
        /// </summary>
        public required Guid EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the document file.
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// Gets or sets the path to the document file.
        /// </summary>
        public string? FilePath { get; set; }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the type of the document file (e.g., PDF, DOCX).
        /// </summary>
        public string? FileType { get; set; }

        /// <summary>
        /// Gets or sets additional notes about the document.
        /// </summary>
        public string? Notes { get; set; }
    }
}
