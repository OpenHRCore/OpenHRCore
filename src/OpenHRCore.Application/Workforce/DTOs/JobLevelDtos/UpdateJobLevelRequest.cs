using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.Workforce.DTOs.JobLevelDtos
{
    public class UpdateJobLevelRequest
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string LevelName { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateJobLevelRequestValidator : AbstractValidator<UpdateJobLevelRequest>
    {
        public UpdateJobLevelRequestValidator(IStringLocalizer<SharedResource> localizedizer)
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
