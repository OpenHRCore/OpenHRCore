using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.CareerConnect.DTOs.JobPostDtos
{
    public class CreateJobPostRequest
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
    }

    public class CreateJobPostRequestValidator : AbstractValidator<CreateJobPostRequest>
    {
        public CreateJobPostRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
