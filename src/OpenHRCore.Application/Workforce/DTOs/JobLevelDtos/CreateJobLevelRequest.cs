using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.Workforce.DTOs.JobLevelDtos
{
    public class CreateJobLevelRequest
    {
        public required string Code { get; set; }
        public required string LevelName { get; set; }
        public string? Description { get; set; }
    }

    public class CreateJobLevelRequestValidator : AbstractValidator<CreateJobLevelRequest>
    {
        public CreateJobLevelRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.LevelName)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
