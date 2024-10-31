using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using OpenHRCore.SharedKernel.Application;
using System.Net;

namespace OpenHRCore.API.Common
{
    /// <summary>
    /// Helper class for creating standardized API responses with consistent status codes and formats.
    /// This class provides methods to generate IActionResult responses for various scenarios including
    /// success, failure, validation errors, and internal server errors.
    /// </summary>
    public static class OpenHRCoreApiResponseHelper
    {
        /// <summary>
        /// Creates a successful API response with HTTP 200 OK status code.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="response">The service response containing the data to be returned.</param>
        /// <returns>An IActionResult with HTTP 200 OK status code.</returns>
        public static IActionResult CreateSuccessResponse<T>(OpenHRCoreServiceResponse<T> response) where T : class
        {
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.OK };
        }

        /// <summary>
        /// Creates a successful API response with a custom status code.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="response">The service response containing the data to be returned.</param>
        /// <param name="statusCode">The HTTP status code to be used in the response.</param>
        /// <returns>An IActionResult with the specified status code.</returns>
        public static IActionResult CreateSuccessResponse<T>(OpenHRCoreServiceResponse<T> response, int statusCode) where T : class
        {
            return new ObjectResult(response) { StatusCode = statusCode };
        }

        /// <summary>
        /// Creates a failure API response with HTTP 400 Bad Request status code.
        /// </summary>
        /// <typeparam name="T">The type of the response data.</typeparam>
        /// <param name="response">The service response containing error details.</param>
        /// <returns>An IActionResult with HTTP 400 Bad Request status code.</returns>
        public static IActionResult CreateFailureResponse<T>(OpenHRCoreServiceResponse<T> response) where T : class
        {
            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest };
        }

        /// <summary>
        /// Creates a validation error API response with a single error message.
        /// </summary>
        /// <param name="errorMessage">The validation error message.</param>
        /// <returns>An IActionResult with HTTP 400 Bad Request status code and validation error details.</returns>
        public static IActionResult CreateValidationErrorResponse(string errorMessage)
        {
            return CreateValidationErrorResponseInternal(errorMessage);
        }

        /// <summary>
        /// Creates a validation error API response from FluentValidation results.
        /// </summary>
        /// <param name="validationResult">The validation result containing error details.</param>
        /// <returns>An IActionResult with HTTP 400 Bad Request status code and validation error details.</returns>
        public static IActionResult CreateValidationErrorResponse(ValidationResult validationResult)
        {
            var errorMessages = string.Join("\n", validationResult.Errors.Select(e => e.ErrorMessage));
            return CreateValidationErrorResponseInternal(errorMessages);
        }

        /// <summary>
        /// Creates an error API response for internal server errors with HTTP 500 status code.
        /// </summary>
        /// <param name="exception">The exception that caused the error.</param>
        /// <returns>An IActionResult with HTTP 500 Internal Server Error status code.</returns>
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

        /// <summary>
        /// Internal helper method to create consistent validation error responses.
        /// </summary>
        /// <param name="errorMessage">The validation error message(s).</param>
        /// <returns>An IActionResult with validation error details.</returns>
        private static IActionResult CreateValidationErrorResponseInternal(string errorMessage)
        {
            return new ObjectResult(new
            {
                Message = "Validation Error.",
                ErrorMessage = errorMessage,
                IsSuccess = false
            })
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
