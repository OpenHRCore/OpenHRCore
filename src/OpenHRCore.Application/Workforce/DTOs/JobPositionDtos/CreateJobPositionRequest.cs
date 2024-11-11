using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.Workforce.DTOs.JobPositionDtos
{
    public class CreateJobPositionRequest
    {
        public required string Code { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required Guid JobLevelId { get; set; }
        public required Guid JobGradeId { get; set; }
        public required Guid OrganizationUnitId { get; set; }
    }

    public class CreateJobPositionRequestValidator : AbstractValidator<CreateJobPositionRequest>
    {
        public CreateJobPositionRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.JobLevelId)
                 .NotEmpty()
                 .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.JobGradeId)
                 .NotEmpty()
                 .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.OrganizationUnitId)
                 .NotEmpty()
                 .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
