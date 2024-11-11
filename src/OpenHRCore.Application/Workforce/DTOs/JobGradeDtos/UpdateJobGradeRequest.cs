using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.Workforce.DTOs.JobGradeDtos
{
    public class UpdateJobGradeRequest
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string GradeName { get; set; }
        public string? Description { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
    }

    public class UpdateJobGradeRequestValidator : AbstractValidator<UpdateJobGradeRequest>
    {
        public UpdateJobGradeRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.GradeName)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
