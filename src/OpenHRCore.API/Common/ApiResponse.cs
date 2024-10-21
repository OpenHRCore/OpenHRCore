using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OpenHRCore.SharedKernel.Application;
using System.Net;

namespace OpenHRCore.API.Common
{
    /// <summary>
    /// Helper class for creating standardized API responses.
    /// </summary>
    public static class ApiResponseHelper
    {
        /// <summary>
        /// Creates a successful API response.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="response">The service response to be wrapped.</param>
        /// <returns>An IActionResult representing a successful response.</returns>
        public static IActionResult CreateSuccessResponse<T>(OpenHRCoreServiceResponse<T> response) where T : class
        {
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.OK };
        }

        /// <summary>
        /// Creates a failure API response.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="response">The service response to be wrapped.</param>
        /// <returns>An IActionResult representing a failure response.</returns>
        public static IActionResult CreateFailureResponse<T>(OpenHRCoreServiceResponse<T> response) where T : class
        {
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest };
        }

        /// <summary>
        /// Creates a validation error API response.
        /// </summary>
        /// <param name="validationResult">The validation result containing error details.</param>
        /// <returns>An IActionResult representing a validation error response.</returns>
        public static IActionResult CreateValidationErrorResponse(ValidationResult validationResult)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));

            return new ObjectResult(new
            {
                Message = "Validation Error.",
                ErrorMessage = errorMessages,
                IsSuccess = false
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        /// <summary>
        /// Creates an error API response for internal server errors.
        /// </summary>
        /// <param name="exception">The exception that caused the error.</param>
        /// <returns>An IActionResult representing an internal server error response.</returns>
        public static IActionResult CreateErrorResponse(Exception exception)
        {
            return new ObjectResult(new
            {
                ErrorMessage = exception.Message,
                Message = "An internal error occurred.",
                IsSuccess = false,
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }
}
