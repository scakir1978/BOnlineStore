using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared.Dtos
{
    public class Error
    {
        public int ErrorCode { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public Error? InnerError { get; set; }

        //Static factory methods
        public static Error CreateError(int errorCode, string message)
        {
            return new Error { ErrorCode = errorCode, Message = message };
        }

        public static Error CreateError(int errorCode, string message, string? stackTrace)
        {
            return new Error { ErrorCode = errorCode, Message = message, StackTrace = stackTrace };
        }

        public static Error CreateError(int errorCode, string message, string? stackTrace, Error? innerError)
        {
            return new Error { ErrorCode = errorCode, Message = message, StackTrace = stackTrace, InnerError = innerError };
        }

    }
}
