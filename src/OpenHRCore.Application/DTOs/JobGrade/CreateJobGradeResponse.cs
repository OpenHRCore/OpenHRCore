namespace OpenHRCore.Application.DTOs.JobGrade
{
    /// <summary>
    /// Represents the response after successfully creating a new job grade.
    /// </summary>
    public class CreateJobGradeResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateJobGradeResponse"/> class.
        /// </summary>
        /// <param name="code">The unique code of the created job grade.</param>
        /// <param name="name">The name of the created job grade.</param>
        /// <param name="description">The optional description of the created job grade.</param>
        /// <param name="sortOrder">The sort order of the created job grade.</param>
        public CreateJobGradeResponse(string code, string name, string? description, int sortOrder)
        {
            Code = code;
            Name = name;
            Description = description;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Gets or sets the unique code of the job grade.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the job grade.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optional description of the job grade.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the sort order of the job grade.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
