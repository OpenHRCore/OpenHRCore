namespace OpenHRCore.SharedKernel.Application.Common
{
    /// <summary>
    /// Represents a standardized response for service operations in the OpenHRCore application.
    /// </summary>
    /// <typeparam name="TData">The type of the data contained in the response.</typeparam>
    public class OpenHRCoreServiceResponse<TData> where TData : class
    {
        /// <summary>
        /// Gets or sets the data of the response.
        /// </summary>
        public TData? Data { get; set; }

        /// <summary>
        /// Gets or sets the error message if the operation failed.
        /// </summary>
        public string? ErrorMessage { get; set; }
        public string? TechnicalMessage { get; set; }
        /// <summary>
        /// Gets or sets a user-friendly message about the operation.
        /// </summary>
        public string? UserMessage { get; set; }

        /// <summary>
        /// Indicates if the response is successful.
        /// </summary>
        public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage);

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreServiceResponse{TData}"/> class.
        /// </summary>
        public OpenHRCoreServiceResponse() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreServiceResponse{TData}"/> class with the specified data.
        /// </summary>
        /// <param name="data">The data to be included in the response.</param>
        public OpenHRCoreServiceResponse(TData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenHRCoreServiceResponse{TData}"/> class with an error message.
        /// </summary>
        /// <param name="errorMessage">The error message describing the failure.</param>
        public OpenHRCoreServiceResponse(string errorMessage)
        {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException(nameof(errorMessage));
        }

        /// <summary>
        /// Provides a detailed string representation of the response.
        /// </summary>
        /// <returns>A string representation of the response.</returns>
        public override string ToString()
        {
            return IsSuccess
                ? $"Success: {UserMessage}, Data: {Data}"
                : $"Error: {ErrorMessage}, Message: {UserMessage}";
        }
    }
}
