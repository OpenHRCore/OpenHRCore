using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Enums;

namespace OpenHRCore.Infrastructure.Data.SeedData;

public static class OpenHRCoreSeedData
{
    private static readonly DateTime _utcNow = DateTime.UtcNow;

    public static readonly Guid HeadOfficeId = Guid.Parse("71c7e3fc-664a-4c1f-8d45-9e6c99f25316");
    public static readonly Guid HrDepartmentId = Guid.Parse("72c7e3fc-664b-4c1f-8d45-9e6c99f25317");
    public static readonly Guid ItDepartmentId = Guid.Parse("73c7e3fc-664c-4c1f-8d45-9e6c99f25318");
    
    public static readonly Guid EntryLevelId = Guid.Parse("81c7e3fc-664d-4c1f-8d45-9e6c99f25316");
    public static readonly Guid JuniorLevelId = Guid.Parse("82c7e3fc-664e-4c1f-8d45-9e6c99f25317");
    public static readonly Guid SeniorLevelId = Guid.Parse("83c7e3fc-664f-4c1f-8d45-9e6c99f25318");
    
    public static readonly Guid Grade1Id = Guid.Parse("91c7e3fc-6641-4c1f-8d45-9e6c99f25316");
    public static readonly Guid Grade2Id = Guid.Parse("92c7e3fc-6642-4c1f-8d45-9e6c99f25317");
    public static readonly Guid Grade3Id = Guid.Parse("93c7e3fc-6643-4c1f-8d45-9e6c99f25318");
    
    public static readonly Guid HrManagerPositionId = Guid.Parse("a1c7e3fc-6644-4c1f-8d45-9e6c99f25316");
    public static readonly Guid SoftwareEngineerPositionId = Guid.Parse("a2c7e3fc-6645-4c1f-8d45-9e6c99f25317");
    
    public static IEnumerable<OrganizationUnit> OrganizationUnits => new[]
    {
        new OrganizationUnit
        {
            Id = HeadOfficeId,
            Code = "HO",
            Name = "Head Office",
            Description = "Main headquarters",
            Location = "New York",
            ParentOrganizationUnitId = null,
            SortOrder = 1,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new OrganizationUnit
        {
            Id = HrDepartmentId,
            Code = "HR",
            Name = "Human Resources",
            Description = "HR Department",
            Location = "New York",
            ParentOrganizationUnitId = HeadOfficeId,
            SortOrder = 2,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new OrganizationUnit
        {
            Id = ItDepartmentId,
            Code = "IT",
            Name = "Information Technology",
            Description = "IT Department",
            Location = "New York",
            ParentOrganizationUnitId = HeadOfficeId,
            SortOrder = 3,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        }
    };

    public static IEnumerable<JobLevel> JobLevels => new[]
    {
        new JobLevel
        {
            Id = EntryLevelId,
            Code = "L1",
            LevelName = "Entry Level",
            Description = "Entry level position",
            SortOrder = 1,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new JobLevel
        {
            Id = JuniorLevelId,
            Code = "L2",
            LevelName = "Junior Level",
            Description = "Junior level position",
            SortOrder = 2,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new JobLevel
        {
            Id = SeniorLevelId,
            Code = "L3",
            LevelName = "Senior Level",
            Description = "Senior level position",
            SortOrder = 3,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        }
    };

    public static IEnumerable<JobGrade> JobGrades => new[]
    {
        new JobGrade
        {
            Id = Grade1Id,
            Code = "G1",
            GradeName = "Grade 1",
            Description = "Entry level grade",
            MinSalary = 30000,
            MaxSalary = 45000,
            SortOrder = 1,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new JobGrade
        {
            Id = Grade2Id,
            Code = "G2",
            GradeName = "Grade 2",
            Description = "Mid level grade",
            MinSalary = 45001,
            MaxSalary = 65000,
            SortOrder = 2,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new JobGrade
        {
            Id = Grade3Id,
            Code = "G3",
            GradeName = "Grade 3",
            Description = "Senior level grade",
            MinSalary = 65001,
            MaxSalary = 90000,
            SortOrder = 3,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        }
    };

    public static IEnumerable<JobPosition> JobPositions => new[]
    {
        new JobPosition
        {
            Id = HrManagerPositionId,
            Code = "HRM",
            JobTitle = "HR Manager",
            Description = "Human Resources Manager",
            OrganizationUnitId = HrDepartmentId,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new JobPosition
        {
            Id = SoftwareEngineerPositionId,
            Code = "SWE",
            JobTitle = "Software Engineer",
            Description = "Software Engineer",
            OrganizationUnitId = ItDepartmentId,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        }
    };

    public static IEnumerable<Employee> Employees => new[]
    {
        new Employee
        {
            Id = Guid.Parse("b1c7e3fc-6646-4c1f-8d45-9e6c99f25316"),
            Code = "EMP001",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateOnly(1985, 5, 15),
            Gender = Gender.Male,
            Email = "john.doe@example.com",
            Phone = "1234567890",
            Address = "123 Main St, New York",
            JobLevelId = SeniorLevelId,
            JobGradeId = Grade3Id,
            JobPositionId = HrManagerPositionId,
            OrganizationUnitId = HrDepartmentId,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        },
        new Employee
        {
            Id = Guid.Parse("b2c7e3fc-6647-4c1f-8d45-9e6c99f25317"),
            Code = "EMP002",
            FirstName = "Jane",
            LastName = "Smith",
            DateOfBirth = new DateOnly(1990, 8, 20),
            Gender = Gender.Female,
            Email = "jane.smith@example.com",
            Phone = "0987654321",
            Address = "456 Oak St, New York",
            JobLevelId = JuniorLevelId,
            JobGradeId = Grade2Id,
            JobPositionId = SoftwareEngineerPositionId,
            OrganizationUnitId = ItDepartmentId,
            CreatedAt = _utcNow,
            UpdatedAt = _utcNow
        }
    };
} 