using AutoMapper;
using Microsoft.Extensions.Logging;
using OpenHRCore.Application.UnitOfWork;
using OpenHRCore.Application.Workforce.DTOs.OU;
using OpenHRCore.Application.Workforce.Interfaces;
using OpenHRCore.Domain.Workforce.Entities;
using OpenHRCore.Domain.Workforce.Interfaces;
using OpenHRCore.SharedKernel.Utilities;

namespace OpenHRCore.Application.Workforce.Services
{
    /// <summary>
    /// Service responsible for managing organization units, including CRUD operations and hierarchy management.
    /// Provides functionality for creating, reading, updating and deleting organization units,
    /// as well as managing their hierarchical relationships and sort orders.
    /// </summary>
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationUnitService> _logger;

        /// <summary>
        /// Initializes a new instance of the OrganizationUnitService class with required dependencies
        /// </summary>
        /// <param name="unitOfWork">Unit of work for transaction management</param>
        /// <param name="organizationUnitRepository">Repository for organization unit data access</param>
        /// <param name="mapper">AutoMapper instance for object mapping</param>
        /// <param name="logger">Logger for service diagnostics</param>
        /// <exception cref="ArgumentNullException">Thrown when any required dependency is null</exception>
        public OrganizationUnitService(
            IOpenHRCoreUnitOfWork unitOfWork,
            IOrganizationUnitRepository organizationUnitRepository,
            IMapper mapper,
            ILogger<OrganizationUnitService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _organizationUnitRepository = organizationUnitRepository ?? throw new ArgumentNullException(nameof(organizationUnitRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates a new organization unit with automatically assigned sort order
        /// </summary>
        /// <param name="request">Details of the organization unit to create</param>
        /// <returns>Response containing the created organization unit details</returns>
        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> CreateOrganizationUnitAsync(CreateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("Beginning organization unit creation process for unit: {OrganizationUnitName}", request.Name);

            try
            {
                var organizationUnit = _mapper.Map<OrganizationUnit>(request);
                organizationUnit.SortOrder = await GetNextSortOrderAsync();
                organizationUnit.CreatedAt = DateTime.UtcNow;

                await _organizationUnitRepository.AddAsync(organizationUnit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Organization unit creation completed successfully. Unit ID: {OrganizationUnitId}, Sort Order: {SortOrder}",
                    organizationUnit.Id, organizationUnit.SortOrder);

                var response = await GetOrganizationUnitResponseById(organizationUnit.Id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Organization unit creation failed for unit name: {OrganizationUnitName}. Error details: {ErrorMessage}", 
                    request.Name, ex.Message);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while creating the Organization Unit.");
            }
        }

        /// <summary>
        /// Retrieves all organization units with their complete hierarchical structure.
        /// Returns a tree-like structure showing parent-child relationships between units.
        /// </summary>
        /// <returns>Response containing collection of organization units with hierarchy information</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsWithHierarchyAsync()
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of complete organization unit hierarchy");

                var organizationUnits = await _organizationUnitRepository.GetAllOrganizationUnitsWithHierarchyAsync(null);
                var organizationUnitsResponse = _mapper.Map<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>(organizationUnits);

                _logger.LogLayerInfo("Successfully retrieved organization unit hierarchy. Total units found: {Count}", organizationUnits.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateSuccess(
                    organizationUnitsResponse,
                    "Retrieved all organization units successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve organization unit hierarchy. Error details: {ErrorMessage}", ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving organization units.");
            }
        }

        /// <summary>
        /// Updates an existing organization unit with the provided details
        /// </summary>
        /// <param name="request">The updated organization unit information</param>
        /// <returns>Response containing the updated organization unit details</returns>
        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> UpdateOrganizationUnitAsync(UpdateOrganizationUnitRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Beginning update process for organization unit ID: {OrganizationUnitId}", request.Id);

                var existingOrganizationUnit = await _organizationUnitRepository.GetByIdAsync(request.Id);
                if (existingOrganizationUnit == null)
                {
                    _logger.LogLayerWarning("Update failed - Unable to locate organization unit with ID: {OrganizationUnitId}", request.Id);
                    return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure("Organization Unit not found.");
                }

                _mapper.Map(request, existingOrganizationUnit);
                existingOrganizationUnit.UpdatedAt = DateTime.UtcNow;

                _organizationUnitRepository.Update(existingOrganizationUnit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Organization unit update completed successfully for ID: {OrganizationUnitId}", request.Id);

                var response = await GetOrganizationUnitResponseById(existingOrganizationUnit.Id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Organization unit update failed for ID: {OrganizationUnitId}. Error details: {ErrorMessage}", 
                    request.Id, ex.Message);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while updating the Organization Unit.");
            }
        }

        /// <summary>
        /// Deletes an organization unit by its ID
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to delete</param>
        /// <returns>Response containing the deleted organization unit details</returns>
        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> DeleteOrganizationUnitAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Beginning deletion process for organization unit ID: {OrganizationUnitId}", id);

                var organizationUnit = await _organizationUnitRepository.GetByIdAsync(id);
                if (organizationUnit == null)
                {
                    _logger.LogLayerWarning("Deletion failed - Unable to locate organization unit with ID: {OrganizationUnitId}", id);
                    return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure("Organization Unit not found.");
                }

                _organizationUnitRepository.Remove(organizationUnit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Organization unit successfully deleted. ID: {OrganizationUnitId}", id);

                var response = _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Organization unit deletion failed for ID: {OrganizationUnitId}. Error details: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while deleting the Organization Unit.");
            }
        }

        /// <summary>
        /// Retrieves an organization unit by its ID including parent relationships
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit to retrieve</param>
        /// <returns>Response containing the requested organization unit details</returns>
        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> GetOrganizationUnitByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of organization unit with ID: {OrganizationUnitId}", id);

                var organizationUnit = await _organizationUnitRepository.GetFirstOrDefaultAsync(
                    x => x.Id == id,
                    x => x.ParentOrganizationUnit!
                    );

                if (organizationUnit == null)
                {
                    _logger.LogLayerWarning("Retrieval failed - Unable to locate organization unit with ID: {OrganizationUnitId}", id);
                    return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure("Organization Unit not found.");
                }

                var response = _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);

                _logger.LogLayerInfo("Successfully retrieved organization unit details for ID: {OrganizationUnitId}", id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve organization unit with ID: {OrganizationUnitId}. Error details: {ErrorMessage}", 
                    id, ex.Message);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Organization Unit.");
            }
        }

        /// <summary>
        /// Retrieves all child organization units for a given parent ID
        /// </summary>
        /// <param name="parentId">The unique identifier of the parent organization unit</param>
        /// <returns>Response containing collection of child organization units with hierarchy information</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsByParentId(Guid parentId)
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of child organization units for parent ID: {ParentId}", parentId);

                var organizationUnits = await _organizationUnitRepository.GetAllOrganizationUnitsWithHierarchyAsync(parentId);
                if (organizationUnits == null || !organizationUnits.Any())
                {
                    _logger.LogLayerWarning("No child organization units found for parent ID: {ParentId}", parentId);
                    return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateFailure("No Organization Units found.");
                }

                var response = _mapper.Map<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>(organizationUnits);

                _logger.LogLayerInfo("Successfully retrieved {Count} child organization units for parent ID: {ParentId}", 
                    organizationUnits.Count, parentId);

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateSuccess(
                    response,
                    "Organization Units retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve child organization units for parent ID: {ParentId}. Error details: {ErrorMessage}", 
                    parentId, ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Organization Units.");
            }
        }

        /// <summary>
        /// Retrieves all organization units without hierarchy information
        /// </summary>
        /// <returns>Response containing a flat collection of all organization units</returns>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>> GetAllOrganizationUnitsAsync()
        {
            try
            {
                _logger.LogLayerInfo("Initiating retrieval of all organization units");

                var organizationUnits = await _organizationUnitRepository.GetAllAsync();
                if (organizationUnits == null || !organizationUnits.Any())
                {
                    _logger.LogLayerWarning("No organization units found in the system");
                    return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateFailure("No Organization Units found.");
                }

                var response = _mapper.Map<IEnumerable<GetOrganizationUnitResponse>>(organizationUnits);

                _logger.LogLayerInfo("Successfully retrieved complete list of organization units. Total count: {Count}", 
                    organizationUnits.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateSuccess(
                    response,
                    "Organization Units retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Failed to retrieve organization units. Error details: {ErrorMessage}", ex.Message);
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Organization Units.");
            }
        }

        #region private methods
        /// <summary>
        /// Retrieves an organization unit by ID including its parent and child relationships
        /// </summary>
        /// <param name="id">The unique identifier of the organization unit</param>
        /// <returns>Mapped response containing the organization unit details</returns>
        private async Task<GetOrganizationUnitResponse> GetOrganizationUnitResponseById(Guid id)
        {
            var organizationUnit = await _organizationUnitRepository.GetFirstOrDefaultAsync(
                x => x.Id == id,
                x => x.ParentOrganizationUnit!,
                x => x.SubOrganizationUnits
                );
            return _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);
        }

        /// <summary>
        /// Calculates the next available sort order value for new organization units
        /// by finding the maximum existing sort order and incrementing it by one
        /// </summary>
        /// <returns>The next available sort order value</returns>
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogLayerInfo("Calculating next available sort order for new organization unit");

            var maxSortOrder = await _organizationUnitRepository.MaxAsync(ou => ou.SortOrder);
            _logger.LogLayerInfo("Current maximum sort order is: {MaxSortOrder}. Next available will be: {NextSortOrder}", 
                maxSortOrder, maxSortOrder + 1);

            return maxSortOrder + 1;
        }
        #endregion
    }
}