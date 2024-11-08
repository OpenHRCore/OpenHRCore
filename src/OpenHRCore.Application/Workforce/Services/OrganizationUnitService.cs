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
    public class OrganizationUnitService : IOrganizationUnitService
    {
        private readonly IOpenHRCoreUnitOfWork _unitOfWork;
        private readonly IOrganizationUnitRepository _organizationUnitRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrganizationUnitService> _logger;

        public OrganizationUnitService(
            IOpenHRCoreUnitOfWork unitOfWork,
            IOrganizationUnitRepository organizationUnitRepository,
            IMapper mapper,
            ILogger<OrganizationUnitService> logger)
        {
            _unitOfWork = unitOfWork;
            _organizationUnitRepository = organizationUnitRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<OpenHRCoreServiceResponse<GetOrganizationUnitResponse>> CreateOrganizationUnitAsync(CreateOrganizationUnitRequest request)
        {
            _logger.LogLayerInfo("Initiating creation of Organization Unit with name: {OrganizationUnitName}", request.Name);

            try
            {
                var organizationUnit = _mapper.Map<OrganizationUnit>(request);
                organizationUnit.SortOrder = await GetNextSortOrderAsync().ConfigureAwait(false);
                organizationUnit.CreatedAt = DateTime.UtcNow;

                await _organizationUnitRepository.AddAsync(organizationUnit).ConfigureAwait(false);
                await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

                _logger.LogLayerInfo("Successfully created Organization Unit with ID: {OrganizationUnitId} and Sort Order: {SortOrder}", organizationUnit.Id, organizationUnit.SortOrder);

                //var response = _mapper.Map<GetOrganizationUnitResponse>(organizationUnit);
                var response = await GetOrganizationUnitResponseById(organizationUnit.Id);

                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateSuccess(response, "Organization Unit created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while creating the Organization Unit with name: {OrganizationUnitName}", request.Name);
                return OpenHRCoreServiceResponse<GetOrganizationUnitResponse>.CreateFailure(ex, "An error occurred while creating the Organization Unit.");
            }
        }
        private async Task<GetOrganizationUnitResponse> GetOrganizationUnitResponseById(Guid id)
        {
            var organizaiton = await _organizationUnitRepository.GetByIdAsync(id);

            return _mapper.Map<GetOrganizationUnitResponse>(organizaiton);
        }
        private async Task<int> GetNextSortOrderAsync()
        {
            _logger.LogLayerInfo("Calculating next sort order for Organization Units.");

            var maxSortOrder = await _organizationUnitRepository.MaxAsync(ou => ou.SortOrder).ConfigureAwait(false);
            _logger.LogLayerInfo("Retrieved maximum sort order value: {MaxSortOrder}", maxSortOrder);

            return maxSortOrder + 1;
        }

        public async Task<OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>> GetAllOrganizationUnitAsync()
        {
            try
            {
                _logger.LogLayerInfo("Retrieving all OrganizationUnits.");

                var organizationUnits = await _organizationUnitRepository.GetAllOrganizationUnitsAsync();
                var organizationUnitsResponse = _mapper.Map<IEnumerable<GetOrganizationUnitResponse>>(organizationUnits);

                _logger.LogLayerInfo("Retrieved {Count} OrganizationUnits.", organizationUnits.Count());

                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateSuccess(organizationUnitsResponse, "Retrieved all organizaiton unit successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "An error occurred while retrieving organizaiton units.");
                return OpenHRCoreServiceResponse<IEnumerable<GetOrganizationUnitResponse>>.CreateFailure(ex, "An error occurred while retrieving organizaiton unit.");
            }
        }
    }
}