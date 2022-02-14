using LibraryProject.Business.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Net;

namespace LibraryProject.API.Controllers.Common
{
    public class ApiErrorResponse
    {
        [JsonProperty("statuscode", NullValueHandling = NullValueHandling.Ignore)]
        public int? StatusCode { get; set; } = null;

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string? Message { get; set; }

        [JsonProperty("serviceErrorCode", NullValueHandling = NullValueHandling.Ignore)]
        public int? ServiceErrorCode { get; set; } = null;

        [JsonProperty("methodErrorCode", NullValueHandling = NullValueHandling.Ignore)]
        public int? MethodErrorCode { get; set; } = null;
    }

    public abstract class LibraryBaseController : ControllerBase
    {
        protected readonly ILogger<LibraryBaseController> Logger;

        public LibraryBaseController(ILogger<LibraryBaseController> logger)
        {
            Logger = logger;
        }

        protected async Task<ActionResult> TryExecuteAsync<T>(Func<Task<T>> action)
            where T : ActionResult
        {
            try
            {
                return await action();
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        protected ActionResult TryExecute<T>(Func<T> action)
            where T : ActionResult
        {
            try
            {
                return action();
            }
            catch (Exception exception)
            {
                return HandleException(exception);
            }
        }

        protected virtual ActionResult HandleException(Exception exception)
        {
            switch (exception)
            {
                case BusinessException e:
                    return LogInfoAndReturn(exception, e.HttpStatusCode);

                case ArgumentException _:
                    return LogErrorAndReturn(exception, HttpStatusCode.BadRequest);

                default:
                    return LogErrorAndReturn(exception, HttpStatusCode.InternalServerError);
            }
        }

        protected ActionResult LogErrorAndReturn(Exception error, HttpStatusCode code)
        {
            Logger.LogError(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        protected ActionResult LogWarningAndReturn(Exception error, HttpStatusCode code)
        {
            Logger.LogWarning(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        protected ActionResult LogInfoAndReturn(Exception error, HttpStatusCode code)
        {
            Logger.LogInformation(error, GetErrorString(error));
            return StatusCode((int)code, GetReturnResponse(error, code));
        }

        private string GetErrorString(Exception error)
        {
            return $"{error.GetType().Name}:{error.Message}";
        }

        private ApiErrorResponse GetReturnResponse(Exception e, HttpStatusCode code)
        {
            return new ApiErrorResponse()
            {
                StatusCode = (int)code,
                Message = e.Message
            };
        }

        private ApiErrorResponse GetReturnResponse(HttpStatusCode code)
        {
            return new ApiErrorResponse()
            {
                StatusCode = (int)code,
                Message = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError)
            };
        }
    }
}