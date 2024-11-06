namespace OpenHRCore.Domain.EmployeeModule.Entities
{
    /// <summary>
    /// Represents a document associated with an employee in the workforce management system.
    /// This class encapsulates all relevant information about an employee's document,
    /// including metadata and file details.
    /// </summary>
    public class EmployeeDocument : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this document.
        /// </summary>
        /// <remarks>
        /// This property establishes a relationship between the document and a specific employee.
        /// It should correspond to the Id of an existing Employee entity.
        /// </remarks>
        public required Guid EmployeeId { get; set; }

        public required virtual Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        /// <remarks>
        /// This should be a descriptive name that clearly identifies the purpose or content of the document.
        /// </remarks>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the document file, including its extension.
        /// </summary>
        /// <remarks>
        /// This should be the original file name as uploaded by the user or system.
        /// </remarks>
        public required string FileName { get; set; }

        /// <summary>
        /// Gets or sets the path to the document file in the storage system.
        /// </summary>
        /// <remarks>
        /// This should be the full path where the file is stored, which may be a local file system path,
        /// a URL for cloud storage, or any other relevant storage identifier.
        /// </remarks>
        public required string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the document file.
        /// </summary>
        /// <remarks>
        /// This should represent the media type of the file, such as "application/pdf" for PDF files
        /// or "image/jpeg" for JPEG images. It helps in determining how to handle or display the file.
        /// </remarks>
        public required string FileType { get; set; }

        /// <summary>
        /// Gets or sets additional notes or comments about the document.
        /// </summary>
        /// <remarks>
        /// This field can be used to store any relevant information about the document that doesn't fit
        /// into other properties, such as context, purpose, or special handling instructions.
        /// </remarks>
        public string? Notes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the document is confidential.
        /// </summary>
        /// <remarks>
        /// When set to true, this flag indicates that the document contains sensitive information
        /// and should be handled with appropriate security measures. This can be used to implement
        /// access control and special handling procedures for confidential documents.
        /// </remarks>
        public bool IsConfidential { get; set; } = false;
    }
}
