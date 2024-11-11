using FluentValidation;
using Microsoft.Extensions.Localization;
using OpenHRCore.Domain.Workforce.Enums;

namespace OpenHRCore.Application.Workforce.DTOs.EmployeeDtos
{
    public class UpdateEmployeeRequest
    {
        public required Guid Id { get; set; }
        public required string Code { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public required Gender Gender { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public string? Address { get; set; }
        public required Guid JobLevelId { get; set; }
        public required Guid JobGradeId { get; set; }
        public required Guid JobPositionId { get; set; }
        public required Guid OrganizationUnitId { get; set; }
    }

    public class UpdateEmployeeRequestValidator : AbstractValidator<UpdateEmployeeRequest>
    {
        public UpdateEmployeeRequestValidator(IStringLocalizer<SharedResource> localizedizer)
        {
            RuleFor(x => x.Id)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Code)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Gender)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.Phone)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.JobLevelId)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.JobGradeId)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.JobPositionId)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.OrganizationUnitId)
               .NotEmpty()
               .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());

            RuleFor(x => x.OrganizationUnitId)
                 .NotEmpty()
                 .WithMessage("{PropertyName} " + localizedizer["RequireField"].Value.ToString());
        }
    }
}
