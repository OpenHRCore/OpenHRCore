using OpenHRCore.Domain.Workforce.Enums;

namespace OpenHRCore.Application.Workforce.DTOs.EmployeeDtos
{
    public class GetEmployeeResponse
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
        public required string JobLevelName { get; set; }
        public required Guid JobGradeId { get; set; }
        public required string JobGradeName { get; set; }
        public required Guid JobPositionId { get; set; }
        public required string JobTitleName { get; set; }
        public required Guid OrganizationUnitId { get; set; }
        public required string OrganizationUnitName { get; set; }
    }
}
