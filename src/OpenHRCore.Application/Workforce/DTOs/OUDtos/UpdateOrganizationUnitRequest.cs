using FluentValidation;
using Microsoft.Extensions.Localization;

namespace OpenHRCore.Application.Workforce.DTOs.OUDtos
{
    public class UpdateOrganizationUnitRequest()
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? Location { get; set; }
        public Guid? ParentOrganizationUnitId { get; set; }
    }


    public class UpdateOrganizationUnitRequestValidator : AbstractValidator<UpdateOrganizationUnitRequest>
    {
        public UpdateOrganizationUnitRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
