using Newtonsoft.Json;

namespace DotFramework.Core.Web
{
    public class BadRequestErrorResult : ErrorResult
    {
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
