namespace BOnlineStore.Shared.Dtos
{
    public class Error
    {
        public string? ErrorCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public Error? InnerError { get; set; }

        //Static factory methods
        public static Error CreateError(string errorCode, string message)
        {
            return new Error { ErrorCode = errorCode, Message = message };
        }

        public static Error CreateError(string errorCode, string message, string? stackTrace)
        {
            return new Error { ErrorCode = errorCode, Message = message, StackTrace = stackTrace };
        }

        public static Error CreateError(string errorCode, string message, string? stackTrace, Error? innerError)
        {
            return new Error { ErrorCode = errorCode, Message = message, StackTrace = stackTrace, InnerError = innerError };
        }

    }
}
