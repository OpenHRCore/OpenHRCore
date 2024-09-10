using OpenHRCore.WorkForce.Application.DTOs.JobGrade;

namespace OpenHRCore.WorkForce.Application.Interfaces
{
    /// <summary>
    /// Defines the contract for job position-related operations.
    /// </summary>
    public interface IJobPositionService
    {
        Task<OpenHRCoreServiceResponse<CreateJobGradeResponse>> CreateJobGradeAsync(CreateJobGradeRequest request);

        Task<OpenHRCoreServiceResponse<UpdateJobGradeResponse>> UpdateJobGradeAsync(UpdateJobGradeRequest request);

        Task<OpenHRCoreServiceResponse<DeleteJobGradeResponse>> DeleteJobGradeAsync(DeleteJobGradeRequest request);

        Task<OpenHRCoreServiceResponse<GetJobGradeByIdResponse>> GetJobGradeByIdAsync(Guid id);

        Task<OpenHRCoreServiceResponse<IEnumerable<GetAllJobGradesResponse>>> GetAllJobGradesAsync();
    }
}
