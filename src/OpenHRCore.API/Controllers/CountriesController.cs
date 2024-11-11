using CountryData.Standard;
using Microsoft.AspNetCore.Mvc;
using OpenHRCore.API.Common;
using OpenHRCore.SharedKernel.Application;

namespace OpenHRCore.API.Controllers
{
    /// <summary>
    /// REST API controller for managing country-related operations.
    /// Provides endpoints for retrieving country information, phone codes, regions and other geographical data.
    /// </summary>
    [ApiController]
    [Route("api/v1/countries")]
    [Produces("application/json")]
    public class CountriesController : ControllerBase
    {
        private readonly CountryHelper _countryHelper;
        private readonly ILogger<CountriesController> _logger;

        /// <summary>
        /// Initializes a new instance of the CountriesController.
        /// </summary>
        /// <param name="countryHelper">Helper service for country-related operations</param>
        /// <param name="logger">Logger instance for the controller</param>
        /// <exception cref="ArgumentNullException">Thrown when countryHelper or logger is null</exception>
        public CountriesController(
            CountryHelper countryHelper,
            ILogger<CountriesController> logger)
        {
            _countryHelper = countryHelper ?? throw new ArgumentNullException(nameof(countryHelper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets a list of all available countries.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with list of country names
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/countries
        /// </remarks>
        [HttpGet]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<IEnumerable<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountries()
        {
            var response = new OpenHRCoreServiceResponse<IEnumerable<string>>();
            _logger.LogLayerInfo("Getting all countries");

            try
            {
                var countries = _countryHelper.GetCountries();

                if (countries == null)
                {
                    const string errorMessage = "Failed to retrieve countries from service";
                    response.ErrorMessage = errorMessage;
                    response.IsSuccess = false;
                    response.Message = errorMessage;

                    _logger.LogLayerWarning("Failed to retrieve countries. No data returned from service.");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                response.IsSuccess = true;
                response.Data = countries;
                response.Message = "Countries retrieved successfully";

                _logger.LogLayerInfo("Successfully retrieved {Count} countries", countries.Count());
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                const string errorMessage = "An error occurred while retrieving countries";
                _logger.LogLayerError(ex, errorMessage);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Gets detailed information for all countries including codes and regions.
        /// </summary>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with collection of detailed country information
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/countries/details
        /// </remarks>
        [HttpGet("details")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<IEnumerable<Country>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountriesDetails()
        {
            var response = new OpenHRCoreServiceResponse<IEnumerable<Country>>();
            _logger.LogLayerInfo("Retrieving detailed country information");

            try
            {
                var countriesData = _countryHelper.GetCountryData();

                if (countriesData == null)
                {
                    const string errorMessage = "Failed to retrieve country details from service";
                    response.ErrorMessage = errorMessage;
                    response.IsSuccess = false;
                    response.Message = errorMessage;

                    _logger.LogLayerWarning("Failed to retrieve country details - Service returned null");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                response.IsSuccess = true;
                response.Data = countriesData;
                response.Message = "Country details retrieved successfully";

                _logger.LogLayerInfo("Successfully retrieved details for {Count} countries", countriesData.Count());
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Exception occurred while retrieving country details: {Message}", ex.Message);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Gets countries by their phone code.
        /// </summary>
        /// <param name="phoneCode">The phone code to search for (e.g., "1" for USA/Canada)</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with collection of countries matching the phone code
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/countries/phone-codes/1
        /// </remarks>
        [HttpGet("phone-codes/{phoneCode}")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<IEnumerable<Country>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountriesByPhoneCode(string phoneCode)
        {
            var response = new OpenHRCoreServiceResponse<IEnumerable<Country>>();
            _logger.LogLayerInfo("Retrieving countries by phone code: {PhoneCode}", phoneCode);

            try
            {
                var countriesData = _countryHelper.GetCountryByPhoneCode(phoneCode);

                if (countriesData == null)
                {
                    const string errorMessage = "Failed to retrieve countries by phone code";
                    response.ErrorMessage = errorMessage;
                    response.IsSuccess = false;
                    response.Message = errorMessage;

                    _logger.LogLayerWarning("Failed to retrieve countries by phone code - Service returned null");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                response.IsSuccess = true;
                response.Data = countriesData;
                response.Message = "Countries retrieved successfully";

                _logger.LogLayerInfo("Successfully retrieved {Count} countries for phone code {PhoneCode}", countriesData.Count(), phoneCode);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Exception occurred while retrieving countries by phone code: {Message}", ex.Message);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Gets the phone code for a country using its ISO code.
        /// </summary>
        /// <param name="isoCode">The country's ISO 3166-1 alpha-2 code (e.g., "US" for United States)</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with the phone code
        /// - 404 Not Found if country not found
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/countries/iso-codes/US/phone-code
        /// </remarks>
        [HttpGet("iso-codes/{isoCode}/phone-code")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<string>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetPhoneCodeByIsoCode(string isoCode)
        {
            var response = new OpenHRCoreServiceResponse<string>();
            _logger.LogLayerInfo("Getting phone code for ISO code: {IsoCode}", isoCode);

            try
            {
                var phoneCode = _countryHelper.GetPhoneCodeByCountryShortCode(isoCode);

                if (phoneCode == null)
                {
                    const string errorMessage = "Failed to retrieve phone code";
                    response.ErrorMessage = errorMessage;
                    response.IsSuccess = false;
                    response.Message = errorMessage;

                    _logger.LogLayerWarning("Failed to retrieve phone code - Service returned null");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                response.IsSuccess = true;
                response.Data = phoneCode;
                response.Message = "Phone code retrieved successfully";

                _logger.LogLayerInfo("Successfully retrieved phone code for country {IsoCode}", isoCode);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                const string errorMessage = "An error occurred while retrieving phone code";
                _logger.LogLayerError(ex, errorMessage);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Gets detailed country information by ISO code.
        /// </summary>
        /// <param name="isoCode">The ISO 3166-1 alpha-2 country code</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with detailed country information
        /// - 404 Not Found if country not found
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/countries/iso-codes/US
        /// </remarks>
        [HttpGet("iso-codes/{isoCode}")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<Country>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCountryByIsoCode(string isoCode)
        {
            var response = new OpenHRCoreServiceResponse<Country>();
            _logger.LogLayerInfo("Retrieving country information for ISO code: {IsoCode}", isoCode);

            try
            {
                var country = _countryHelper.GetCountryByCode(isoCode);

                if (country == null)
                {
                    const string errorMessage = "Failed to retrieve country information";
                    response.ErrorMessage = errorMessage;
                    response.IsSuccess = false;
                    response.Message = errorMessage;

                    _logger.LogLayerWarning("Failed to retrieve country information - Service returned null");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                response.IsSuccess = true;
                response.Data = country;
                response.Message = "Country information retrieved successfully";

                _logger.LogLayerInfo("Successfully retrieved information for country {IsoCode}", isoCode);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Exception occurred while retrieving country information: {Message}", ex.Message);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }

        /// <summary>
        /// Gets all regions/states/provinces for a specific country.
        /// </summary>
        /// <param name="isoCode">The ISO 3166-1 alpha-2 country code</param>
        /// <returns>
        /// ActionResult containing:
        /// - 200 OK with collection of regions
        /// - 404 Not Found if country not found
        /// - 500 Internal Server Error if retrieval fails
        /// </returns>
        /// <remarks>
        /// Sample request:
        /// GET /api/v1/countries/iso-codes/US/regions
        /// </remarks>
        [HttpGet("iso-codes/{isoCode}/regions")]
        [ProducesResponseType(typeof(OpenHRCoreServiceResponse<IEnumerable<Regions>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetRegionsByIsoCode(string isoCode)
        {
            var response = new OpenHRCoreServiceResponse<IEnumerable<Regions>>();
            _logger.LogLayerInfo("Retrieving regions for country: {IsoCode}", isoCode);

            try
            {
                var regions = _countryHelper.GetRegionByCountryCode(isoCode);

                if (regions == null)
                {
                    const string errorMessage = "Failed to retrieve regions";
                    response.ErrorMessage = errorMessage;
                    response.IsSuccess = false;
                    response.Message = errorMessage;

                    _logger.LogLayerWarning("Failed to retrieve regions - Service returned null");
                    return OpenHRCoreApiResponseHelper.CreateFailureResponse(response);
                }

                response.IsSuccess = true;
                response.Data = regions;
                response.Message = "Regions retrieved successfully";

                _logger.LogLayerInfo("Successfully retrieved {Count} regions for country {IsoCode}", regions.Count(), isoCode);
                return OpenHRCoreApiResponseHelper.CreateSuccessResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogLayerError(ex, "Exception occurred while retrieving regions: {Message}", ex.Message);
                return OpenHRCoreApiResponseHelper.CreateErrorResponse(ex);
            }
        }
    }
}
