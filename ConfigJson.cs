using System;
using Newtonsoft.Json;

namespace Bot_Core
{
    public struct ConfigJson
    {
        [JsonProperty("token")]
        public string token {get; private set; }
        [JsonProperty("prefix")]
        public string Prefix {get; private set; }
    }
}