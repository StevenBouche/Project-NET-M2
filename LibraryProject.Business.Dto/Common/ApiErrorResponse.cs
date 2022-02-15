using Newtonsoft.Json;

namespace LibraryProject.Business.Dto.Common
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
}