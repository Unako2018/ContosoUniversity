using Newtonsoft.Json;

namespace QuicklSignManager.Dto.Request;

public class Signatory
{
    /// <summary>
    /// this is our unique identifier, this is also used in our above data_fields to specify who the field should be assigned to
    /// </summary>
    [JsonProperty(PropertyName = "uuid")]
    public string UUID { get; set; }

    /// <summary>
    /// this is our unique identifier, this is also used in our above data_fields to specify who the field should be assigned to
    /// </summary>
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }

    /// <summary>
    /// this is our unique identifier, this is also used in our above data_fields to specify who the field should be assigned to
    /// </summary>
    [JsonProperty(PropertyName = "role")]
    public string Role { get; set; }

    /// <summary>
    /// //mobile number can be ignored if primary_communication_channel is email
    /// (user will receive their OTP via email if use_two_factor_authentication is set 
    /// to true under signing_settings and no mobile numnber is supplied)
    /// </summary>
    [JsonProperty(PropertyName = "mobile_number")]
    public string MobileNumber { get; set; }

    [JsonProperty(PropertyName = "email")]
    public string Email { get; set; }

    /// <summary>
    /// signer to be prompt to sign his/her document via email
    /// </summary>
    [JsonProperty(PropertyName = "primary_communication_channel")]
    public string PrimaryCommunicationChannel { get; set; }
}
