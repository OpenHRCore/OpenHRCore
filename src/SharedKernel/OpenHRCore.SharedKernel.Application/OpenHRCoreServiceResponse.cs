namespace OpenHRCore.SharedKernel.Application
{
    /// <summary>
    /// Represents a generic service response for OpenHRCore operations.
    /// </summary>
    /// <typeparam name="TData">The type of data contained in the response.</typeparam>
    public class OpenHRCoreServiceResponse<TData> where TData : class
    {
        /// <summary>
        /// Gets or sets the data returned by the service operation.
        /// </summary>
        public TData? Data { get; set; }

        /// <summary>
        /// Gets or sets the error message in case of a failure.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the general message for the response.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether the operation was successful.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Creates a successful response with the provided data and message.
        /// </summary>
        /// <param name="data">The data to be included in the response.</param>
        /// <param name="message">The success message.</param>
        /// <returns>A new instance of OpenHRCoreServiceResponse indicating success.</returns>
        public static OpenHRCoreServiceResponse<TData> CreateSuccess(TData data, string message) =>
            new OpenHRCoreServiceResponse<TData>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };

        /// <summary>
        /// Creates a failure response with the provided error message.
        /// </summary>
        /// <param name="errorMessage">The error message describing the failure.</param>
        /// <returns>A new instance of OpenHRCoreServiceResponse indicating failure.</returns>
        public static OpenHRCoreServiceResponse<TData> CreateFailure(string errorMessage) =>
            new OpenHRCoreServiceResponse<TData>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };

        /// <summary>
        /// Creates a failure response with the provided exception and error message.
        /// </summary>
        /// <param name="exception">The exception that caused the failure.</param>
        /// <param name="errorMessage">The error message describing the failure.</param>
        /// <returns>A new instance of OpenHRCoreServiceResponse indicating failure with exception details.</returns>
        public static OpenHRCoreServiceResponse<TData> CreateFailure(Exception exception, string errorMessage) =>
            new OpenHRCoreServiceResponse<TData>
            {
                IsSuccess = false,
                ErrorMessage = exception.Message,
                Message = errorMessage
            };
    }
}
