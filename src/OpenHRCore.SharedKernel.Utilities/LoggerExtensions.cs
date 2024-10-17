using Microsoft.Extensions.Logging;

namespace OpenHRCore.SharedKernel.Utilities
{
    /// <summary>
    /// Provides extension methods for ILogger to log messages with layer-specific prefixes.
    /// </summary>
    public static class LoggerExtensions
    {
        private const string ApiLayerPrefix = "OpenHRCore.API";
        private const string ApplicationLayerPrefix = "OpenHRCore.Application";
        private const string InfrastructureLayerPrefix = "OpenHRCore.Infrastructure";

        #region API Layer

        /// <summary>
        /// Logs an information message for the API layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogApiInfo(this ILogger logger, string message, params object?[] args)
        {
            logger.LogInformation($"[{ApiLayerPrefix}] {message}", args);
        }

        /// <summary>
        /// Logs a warning message for the API layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogApiWarning(this ILogger logger, string message, params object?[] args)
        {
            logger.LogWarning($"[{ApiLayerPrefix}] {message}", args);
        }

        /// <summary>
        /// Logs an error message for the API layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogApiError(this ILogger logger, Exception? exception, string message, params object?[] args)
        {
            logger.LogError(exception, $"[{ApiLayerPrefix}] {message}", args);
        }

        #endregion

        #region Application Layer

        /// <summary>
        /// Logs an information message for the Application layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogApplicationInfo(this ILogger logger, string message, params object?[] args)
        {
            logger.LogInformation($"[{ApplicationLayerPrefix}] {message}", args);
        }

        /// <summary>
        /// Logs a warning message for the Application layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogApplicationWarning(this ILogger logger, string message, params object?[] args)
        {
            logger.LogWarning($"[{ApplicationLayerPrefix}] {message}", args);
        }

        /// <summary>
        /// Logs an error message for the Application layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogApplicationError(this ILogger logger, Exception? exception, string message, params object?[] args)
        {
            logger.LogError(exception, $"[{ApplicationLayerPrefix}] {message}", args);
        }

        #endregion

        #region Infrastructure Layer

        /// <summary>
        /// Logs an information message for the Infrastructure layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogInfrastructureInfo(this ILogger logger, string message, params object?[] args)
        {
            logger.LogInformation($"[{InfrastructureLayerPrefix}] {message}", args);
        }

        /// <summary>
        /// Logs a warning message for the Infrastructure layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogInfrastructureWarning(this ILogger logger, string message, params object?[] args)
        {
            logger.LogWarning($"[{InfrastructureLayerPrefix}] {message}", args);
        }

        /// <summary>
        /// Logs an error message for the Infrastructure layer.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The message to log.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        public static void LogInfrastructureError(this ILogger logger, Exception? exception, string message, params object?[] args)
        {
            logger.LogError(exception, $"[{InfrastructureLayerPrefix}] {message}", args);
        }

        #endregion
    }
}
