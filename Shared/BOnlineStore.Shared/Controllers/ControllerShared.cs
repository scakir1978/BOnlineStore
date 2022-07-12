using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Mvc; 

namespace BOnlineStore.Shared.Controllers
{
    public class ControllerShared : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(T data, HttpStatusCode statusCode)
        {
            var response = Response<T>.Success(data, statusCode);

            return new ObjectResult(response)
            {
                 StatusCode = (int)statusCode
            };
        }
        public IActionResult CreateSuccessActionResultInstance<T>(T data)
        {
            var response = Response<T>.Success(data, HttpStatusCode.OK );

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        public IActionResult CreateInternalServerErrorActionResultInstance<T>(T data)
        {
            var response = Response<T>.Success(data, HttpStatusCode.InternalServerError);

            return new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError               
                
            };
        }

    }
}
