using FluentValidation;

namespace OpenHRCore.Application.DTOs.JobGrade
{
    public class DeleteJobGradeRequest
    {
        public DeleteJobGradeRequest(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }

    public class DeleteJobGradeRequestValidator : AbstractValidator<DeleteJobGradeRequest>
    {
        public DeleteJobGradeRequestValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage("Id is required.");
        }
    }
}
