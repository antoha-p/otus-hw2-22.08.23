using System;
using Newtonsoft.Json;

namespace WebClient;

[Serializable]
public class Customer
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("firstname")]
    public string Firstname { get; set; }

    [JsonProperty("lastname")]
    public string Lastname { get; set; }
}