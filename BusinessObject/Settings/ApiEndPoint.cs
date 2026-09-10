using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace BusinessObject.Settings
{
    public class ApiEndPoint
    {
        [Required] public string BaseUrl { get; set; }
        [Required] public string ApiKey { get; set; }
        [Required] public string AccessUrl { get; set; }
        [Required] public string ClientId { get; set; }
        [Required] public string Scope { get; set; }
        [Required] public string ClientSecret { get; set; }
        [JsonIgnore] public Dictionary<string, string> Headers { get; set; }
        [JsonIgnore] public string GrantType { get; set; }
        [JsonIgnore] public string UserName { get; set; }
        [JsonIgnore] public string Password { get; set; }

    }
}
