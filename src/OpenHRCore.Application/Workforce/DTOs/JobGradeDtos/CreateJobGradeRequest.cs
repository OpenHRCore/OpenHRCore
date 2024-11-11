using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.Workforce.DTOs.JobGradeDtos
{
    public class CreateJobGradeRequest
    {
        public required string Code { get; set; }
        public required string GradeName { get; set; }
        public string? Description { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }

    public class CreateJobGradeRequestValidator : AbstractValidator<CreateJobGradeRequest>
    {
        public CreateJobGradeRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.GradeName)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
