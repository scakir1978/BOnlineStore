using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Dtos
{
    public class ErrorDto
    {
        public int ErrorCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public ErrorDto? InnerError { get; set; }

        //Static factory methods
        public static ErrorDto CreateError(int errorCode, string message)
        {
            return new ErrorDto { ErrorCode = errorCode, Message = message };
        }

        public static ErrorDto CreateError(int errorCode, string message, string? stackTrace)
        {
            return new ErrorDto { ErrorCode = errorCode, Message = message, StackTrace = stackTrace };
        }

        public static ErrorDto CreateError(int errorCode, string message, string? stackTrace, ErrorDto? innerError)
        {
            return new ErrorDto { ErrorCode = errorCode, Message = message, StackTrace = stackTrace, InnerError = innerError };
        }

    }
}
