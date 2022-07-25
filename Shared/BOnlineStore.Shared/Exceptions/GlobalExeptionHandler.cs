using BOnlineStore.Localization;
using BOnlineStore.Localization.Constants;
using BOnlineStore.Shared.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BOnlineStore.Shared.Exceptions
{
    public static class GlobalExeptionHandler
    {        
        public static void ConfigureExeptionHandler(this WebApplication app)
        {

            app.UseExceptionHandler(exceptionHandlerApp =>
            {
                exceptionHandlerApp.Run(async context =>
                {
                    using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
                    {
                        var _stringLocalizer = scope.ServiceProvider.GetRequiredService<IStringLocalizer<Language>>();

                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        // using static System.Net.Mime.MediaTypeNames;
                        context.Response.ContentType = Application.Json;

                        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                        switch (exceptionHandlerPathFeature?.Error)
                        {
                            case FileNotFoundException:
                                var fileNotFoundException = exceptionHandlerPathFeature?.Error as FileNotFoundException;
                                await context.Response.WriteAsync(Response<NoContent>.Fail(
                                    Error.CreateError(nameof(FileNotFoundException),
                                                       exceptionHandlerPathFeature?.Error?.Message ?? "",
                                                       exceptionHandlerPathFeature?.Error.StackTrace
                                                     ),
                                    System.Net.HttpStatusCode.InternalServerError).ToString());
                                break;

                            case FluentValidation.ValidationException:

                                await CreateValidationException(context, exceptionHandlerPathFeature);
                                break;

                            default:

                                await context.Response.WriteAsync(Response<NoContent>.Fail(
                                    Error.CreateError(nameof(exceptionHandlerPathFeature.Error.GetType),
                                                       exceptionHandlerPathFeature?.Error?.Message ?? "",
                                                       exceptionHandlerPathFeature?.Error.StackTrace
                                                     ),
                                    System.Net.HttpStatusCode.InternalServerError).ToString());
                                break;
                        }                    
                    }
                    
                });
            });
        }

        private static async Task CreateValidationException(HttpContext context, IExceptionHandlerPathFeature? exceptionHandlerPathFeature)
        {
            var validationException = exceptionHandlerPathFeature?.Error as FluentValidation.ValidationException;

            List<Error> errors = new List<Error>();

            foreach (var error in validationException.Errors)
            {
                errors.Add(Error.CreateError(error.ErrorCode, error.ErrorMessage, exceptionHandlerPathFeature?.Error.StackTrace));
            }

            await context.Response.WriteAsync(Response<NoContent>.Fail(errors, System.Net.HttpStatusCode.InternalServerError).ToString());
        }
    }
}
