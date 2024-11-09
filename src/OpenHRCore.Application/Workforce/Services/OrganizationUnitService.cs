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
    /// Service for managing organization units
    /// </summary>
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationUnitService> _logger;

        /// <summary>
        /// Initializes a new instance of the OrganizationUnitService class
        /// </summary>
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
        /// Creates a new organization unit
        /// </summary>
        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> CreateOrganizationUnitAsync(CreateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("Initiating creation of Organization Unit with name: {OrganizationUnitName}", request.Name);

            try
            {
                var organizationUnit = _mapper.Map<OrganizationUnit>(request);
                organizationUnit.SortOrder = await GetNextSortOrderAsync();
                organizationUnit.CreatedAt = DateTime.UtcNow;

                await _organizationUnitRepository.AddAsync(organizationUnit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Successfully created Organization Unit with ID: {OrganizationUnitId} and Sort Order: {SortOrder}",
                    organizationUnit.Id, organizationUnit.SortOrder);

                var response = await GetOrganizationUnitResponseById(organizationUnit.Id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while creating the Organization Unit with name: {OrganizationUnitName}", request.Name);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while creating the Organization Unit.");
            }
        }



        /// <summary>
        /// Gets all organization units with their hierarchical structure
        /// </summary>
        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsWithHierarchyAsync()
        {
            try
            {
                _logger.LogLayerInfo("Retrieving all OrganizationUnits.");

                var organizationUnits = await _organizationUnitRepository.GetAllOrganizationUnitsWithHierarchyAsync(null);
                var organizationUnitsResponse = _mapper.Map<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>(organizationUnits);

                _logger.LogLayerInfo("Retrieved {Count} OrganizationUnits.", organizationUnits.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateSuccess(
                    organizationUnitsResponse,
                    "Retrieved all organization units successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving organization units.");
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving organization units.");
            }
        }

        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> UpdateOrganizationUnitAsync(UpdateOrganizationUnitRequest request)
        {
            try
            {
                _logger.LogLayerInfo("Initiating update of Organization Unit with ID: {OrganizationUnitId}", request.Id);

                var existingOrganizationUnit = await _organizationUnitRepository.GetByIdAsync(request.Id);
                if (existingOrganizationUnit == null)
                {
                    _logger.LogLayerWarning("Organization Unit with ID: {OrganizationUnitId} not found", request.Id);
                    return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure("Organization Unit not found.");
                }

                _mapper.Map(request, existingOrganizationUnit);
                existingOrganizationUnit.UpdatedAt = DateTime.UtcNow;

                _organizationUnitRepository.Update(existingOrganizationUnit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Successfully updated Organization Unit with ID: {OrganizationUnitId}", request.Id);

                var response = await GetOrganizationUnitResponseById(existingOrganizationUnit.Id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while updating the Organization Unit with ID: {OrganizationUnitId}", request.Id);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while updating the Organization Unit.");
            }
        }

        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> DeleteOrganizationUnitAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Initiating deletion of Organization Unit with ID: {OrganizationUnitId}", id);

                var organizationUnit = await _organizationUnitRepository.GetByIdAsync(id);
                if (organizationUnit == null)
                {
                    _logger.LogLayerWarning("Organization Unit with ID: {OrganizationUnitId} not found", id);
                    return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure("Organization Unit not found.");
                }

                _organizationUnitRepository.Remove(organizationUnit);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogLayerInfo("Successfully deleted Organization Unit with ID: {OrganizationUnitId}", id);

                var response = _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while deleting the Organization Unit with ID: {OrganizationUnitId}", id);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while deleting the Organization Unit.");
            }
        }

        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> GetOrganizationUnitByIdAsync(Guid id)
        {
            try
            {
                _logger.LogLayerInfo("Retrieving Organization Unit with ID: {OrganizationUnitId}", id);

                var organizationUnit = await _organizationUnitRepository.GetFirstOrDefaultAsync(x => x.Id == id, "ParentOrganizationUnit,SubOrganizationUnits");
                if (organizationUnit == null)
                {
                    _logger.LogLayerWarning("Organization Unit with ID: {OrganizationUnitId} not found", id);
                    return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure("Organization Unit not found.");
                }

                var response = _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);

                _logger.LogLayerInfo("Successfully retrieved Organization Unit with ID: {OrganizationUnitId}", id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(
                    response,
                    "Organization Unit retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving the Organization Unit with ID: {OrganizationUnitId}", id);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Organization Unit.");
            }
        }

        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>> GetAllOrganizationUnitsByParentId(Guid parentId)
        {
            try
            {
                _logger.LogLayerInfo("Retrieving Organization Units by Parent ID: {ParentId}", parentId);

                var organizationUnits = await _organizationUnitRepository.GetAllOrganizationUnitsWithHierarchyAsync(parentId);
                if (organizationUnits == null || !organizationUnits.Any())
                {
                    _logger.LogLayerWarning("No Organization Units found for Parent ID: {ParentId}", parentId);
                    return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateFailure("No Organization Units found.");
                }

                var response = _mapper.Map<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>(organizationUnits);

                _logger.LogLayerInfo("Successfully retrieved {Count} Organization Units for Parent ID: {ParentId}", organizationUnits.Count, parentId);

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateSuccess(
                    response,
                    "Organization Units retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving Organization Units for Parent ID: {ParentId}", parentId);
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitsWithHierarchyResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Organization Units.");
            }
        }

        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>> GetAllOrganizationUnitsAsync()
        {
            try
            {
                _logger.LogLayerInfo("Retrieving all Organization Units");

                var organizationUnits = await _organizationUnitRepository.GetAllAsync();
                if (organizationUnits == null || !organizationUnits.Any())
                {
                    _logger.LogLayerWarning("No Organization Units found");
                    return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateFailure("No Organization Units found.");
                }

                var response = _mapper.Map<IEnumerable<GetOrganizationUnitResponse>>(organizationUnits);

                _logger.LogLayerInfo("Successfully retrieved {Count} Organization Units", organizationUnits.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateSuccess(
                    response,
                    "Organization Units retrieved successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving all Organization Units");
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateFailure(
                    ex,
                    "An error occurred while retrieving the Organization Units.");
            }
        }

        #region private methods
        /// <summary>
        /// Gets an organization unit by ID
        /// </summary>
        private async Task<GetOrganizationUnitResponse> GetOrganizationUnitResponseById(Guid id)
        {
            var organizationUnit = await _organizationUnitRepository.GetFirstOrDefaultAsync(x => x.Id == id, "ParentOrganizationUnit,SubOrganizationUnits");
            return _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);
        }

        /// <summary>
        /// Gets the next available sort order value
        /// </summary>
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogLayerInfo("Calculating next sort order for Organization Units.");

            var maxSortOrder = await _organizationUnitRepository.MaxAsync(ou => ou.SortOrder);
            _logger.LogLayerInfo("Retrieved maximum sort order value: {MaxSortOrder}", maxSortOrder);

            return maxSortOrder + 1;
        }
        #endregion
    }
}