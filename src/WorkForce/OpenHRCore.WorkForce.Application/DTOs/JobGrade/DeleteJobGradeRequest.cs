using FluentValidation;

namespace OpenHRCore.WorkForce.Application.DTOs.JobGrade
{
    public class DeleteJobGradeRequest
    {
        public DeleteJobGradeRequest(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
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
