using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DotFramework.Core.Web
{
    public class ModelStateErrorResult
    {
        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("ModelState")]
        public Dictionary<String, String[]> ModelState { get; set; }
    }
}
