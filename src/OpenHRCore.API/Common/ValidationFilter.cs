using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace OpenHRCore.API.Common
{
    /// <summary>
    /// Implements a validation filter to handle model state validation before action execution.
    /// </summary>
    public class ValidationFilter : IActionFilter
    {
        /// <summary>
        /// Executes after the action method is called. This method is not implemented.
        /// </summary>
        /// <param name="context">The context for action execution.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // This method is not implemented as per the current requirements
        }

        /// <summary>
        /// Executes before the action method is called. Validates the model state and returns a bad request if invalid.
        /// </summary>
        /// <param name="context">The context for action execution.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = GetValidationErrors(context);
                var errorResponse = CreateErrorResponse(validationErrors);
                context.Result = new BadRequestObjectResult(errorResponse);
            }
        }

        /// <summary>
        /// Extracts validation errors from the model state.
        /// </summary>
        /// <param name="context">The context for action execution.</param>
        /// <returns>A dictionary of field names and their corresponding error messages.</returns>
        private static Dictionary<string, string[]> GetValidationErrors(ActionExecutingContext context)
        {
            return context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray() ?? Array.Empty<string>()
                );
        }

        /// <summary>
        /// Creates an error response object.
        /// </summary>
        /// <param name="errors">The dictionary of validation errors.</param>
        /// <returns>An anonymous object representing the error response.</returns>
        private static object CreateErrorResponse(Dictionary<string, string[]> errors)
        {
            return new
            {
                IsSuccess = false,
                Message = "Validation failed",
                Errors = errors,
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
