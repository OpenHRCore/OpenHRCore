using OpenHRCore.Application.Workforce.DTOs.OUDtos;

namespace OpenHRCore.Application.Workforce.Interfaces
{
    /// <summary>
    /// Interface for managing organization units, providing CRUD operations and hierarchy management functionality
    /// </summary>
    public interface IOrganizationUnitService
    {
        /// <summary>
        /// Creates a new organization unit
        /// </summary>
        /// <param name="request">The details of the organization unit to create</param>
        /// <returns>Response containing the created organization unit details</returns>
        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> CreateOrganizationUnitAsync(CreateOrganizationUnitRequest request);

        /// <summary>
        /// Updates an existing organization unit
        /// </summary>
        /// <param name="request">The updated details of the organization unit</param>
        /// <returns>Response containing the updated organization unit details</returns>
        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> UpdateOrganizationUnitAsync(UpdateOrganizationUnitRequest request);

        /// <summary>
        /// Deletes an organization unit by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to delete</param>
        /// <returns>Response indicating the result of the deletion operation</returns>
        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> DeleteOrganizationUnitAsync(Guid id);

        /// <summary>
        /// Retrieves an organization unit by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to retrieve</param>
        /// <returns>Response containing the requested organization unit details</returns>
        Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> GetOrganizationUnitByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all child organization units for a given parent ID
        /// </summary>
        /// <param name="parentId">The unique identifier of the parent organization unit</param>
        /// <returns>Response containing a collection of child organization units with hierarchy information</returns>
        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsByParentId(Guid parentId);

        /// <summary>
        /// Retrieves all organization units with their complete hierarchical structure
        /// </summary>
        /// <returns>Response containing a collection of organization units with hierarchy information</returns>
        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsWithHierarchyAsync();

        /// <summary>
        /// Retrieves all organization units without hierarchy information
        /// </summary>
        /// <returns>Response containing a flat collection of all organization units</returns>
        Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>> GetAllOrganizationUnitsAsync();
    }
}
