namespace OpenHRCore.Domain.EmployeeModule.Entities
{
    /// <summary>
    /// Represents the educational background of an employee in the OpenHR Core system.
    /// This entity encapsulates all relevant information about an employee's education,
    /// including the institution, degree, field of study, and duration.
    /// </summary>
    public class EmployeeEducation : OpenHRCoreBaseEntity
    {
        /// <summary>
        /// Gets or sets the unique identifier of the employee associated with this education record.
        /// </summary>
        /// <remarks>
        /// This property is required and establishes a relationship between the education record
        /// and the corresponding employee in the system.
        /// </remarks>
        public required Guid EmployeeId { get; set; }

        public required virtual Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the name of the educational institution where the employee studied.
        /// </summary>
        /// <remarks>
        /// This property is required and should contain the full, official name of the institution.
        /// </remarks>
        public required string InstitutionName { get; set; }

        /// <summary>
        /// Gets or sets the degree or certification obtained by the employee.
        /// </summary>
        /// <remarks>
        /// This property is required and should contain the full title of the degree or certification,
        /// e.g., "Bachelor of Science", "Master of Arts", "Professional Certification in Project Management".
        /// </remarks>
        public required string Degree { get; set; }

        /// <summary>
        /// Gets or sets the specific field of study for the degree or certification.
        /// </summary>
        /// <remarks>
        /// This property is required and should provide a clear description of the academic discipline
        /// or area of specialization, e.g., "Computer Science", "Business Administration", "Mechanical Engineering".
        /// </remarks>
        public required string FieldOfStudy { get; set; }

        /// <summary>
        /// Gets or sets the start date of the education program.
        /// </summary>
        /// <remarks>
        /// This property is required and represents the date when the employee began their studies
        /// at the specified institution.
        /// </remarks>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the education program.
        /// </summary>
        /// <remarks>
        /// This property is optional and represents the date when the employee completed or is expected
        /// to complete their studies. It can be null if the education is ongoing or the end date is unknown.
        /// </remarks>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the grade or GPA achieved by the employee.
        /// </summary>
        /// <remarks>
        /// This property is optional and can be used to store the final grade, GPA, or any other
        /// measure of academic performance. The format may vary depending on the institution's grading system.
        /// </remarks>
        public string? Grade { get; set; }

        /// <summary>
        /// Gets or sets any additional notes or comments related to the education record.
        /// </summary>
        /// <remarks>
        /// This property is optional and can be used to store any relevant information that doesn't
        /// fit into the other properties, such as honors received, special projects completed, or
        /// any other noteworthy aspects of the employee's educational experience.
        /// </remarks>
        public string? Notes { get; set; }
    }
}
