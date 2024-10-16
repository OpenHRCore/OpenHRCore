using FluentValidation;

namespace OpenHRCore.Application.DTOs.JobGrade
{
    /// <summary>
    /// Represents a request to create a new job grade.
    /// </summary>
    public class CreateJobGradeRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateJobGradeRequest"/> class.
        /// </summary>
        /// <param name="code">The unique code for the job grade.</param>
        /// <param name="name">The name of the job grade.</param>
        /// <param name="description">An optional description of the job grade.</param>
        public CreateJobGradeRequest(string code, string name, string? description = null)
        {
            Code = code;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Gets or sets the unique code for the job grade.
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
    }

    /// <summary>
    /// Validator for the <see cref="CreateJobGradeRequest"/> class.
    /// </summary>
    public class CreateJobGradeRequestValidator : AbstractValidator<CreateJobGradeRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateJobGradeRequestValidator"/> class.
        /// </summary>
        public CreateJobGradeRequestValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Code is required.")
                .Length(1, 50).WithMessage("Code must be between 1 and 50 characters.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(250).WithMessage("Description cannot be longer than 250 characters.");
        }
    }
}
