namespace OpenHRCore.SharedKernel.Application
{
    public class OpenHRCoreServiceResponse<TData> where TData : class
    {
        public TData? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; } = true;

        public static OpenHRCoreServiceResponse<TData> CreateSuccess(TData data, string message) =>
            new OpenHRCoreServiceResponse<TData>
            {
                IsSuccess = true,
                Data = data,
                Message = message
            };

        public static OpenHRCoreServiceResponse<TData> CreateFailure(string errorMessage) =>
            new OpenHRCoreServiceResponse<TData>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage
            };

        public static OpenHRCoreServiceResponse<TData> CreateFailure(Exception exception, string errorMessage) =>
            new OpenHRCoreServiceResponse<TData>
            {
                IsSuccess = false,
                ErrorMessage = exception.Message,
                Message = errorMessage
            };
    }
}