using FluentValidation;

namespace OpenHRCore.Application.DTOs.JobGrade
{
    /// <summary>
    /// Represents the request to update a job grade.
    /// </summary>
    public class UpdateJobGradeRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateJobGradeRequest"/> class.
        /// </summary>
        /// <param name="id">The ID of the job grade.</param>
        /// <param name="code">The code of the job grade.</param>
        /// <param name="name">The name of the job grade.</param>
        /// <param name="description">The description of the job grade.</param>
        public UpdateJobGradeRequest(Guid id, string code, string name, string? description)
        {
            Id = id;
            Code = code;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Gets or sets the ID of the job grade.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the code of the job grade.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the name of the job grade.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the job grade.
        /// </summary>
        public string? Description { get; set; }
    }

    /// <summary>
    /// Represents the validator for the <see cref="UpdateJobGradeRequest"/> class.
    /// </summary>
    public class UpdateJobGradeRequestValidator : AbstractValidator<UpdateJobGradeRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateJobGradeRequestValidator"/> class.
        /// </summary>
        public UpdateJobGradeRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Id is required.");

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
