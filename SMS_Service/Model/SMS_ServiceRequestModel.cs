using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace SMS_Service.Model
{
    public class SMS_ServiceRequestModel
    {
        [JsonProperty("@VER")]
        public string Ver { get; set; } = string.Empty;
        [JsonProperty("USER")]
        public UserData User { get; set; }
        [JsonProperty("DLR")]
        public DLRData Dlr { get; set; }
        [JsonProperty("SMS")]
        public List<SMSData> Sms { get; set; }
    }

    public class UserData
    {
        [JsonProperty("@USERNAME")]
        public string UserName { get; set; } = string.Empty;
        [JsonProperty("@PASSWORD")]
        public string Password { get; set; } = string.Empty;
        [JsonProperty("@UNIXTIMESTAMP")]
        public string UnixTimestamp{ get; set; } = string.Empty;
    }

    public class DLRData
    {
        [JsonProperty("@URL")]
        public string Url { get; set; } = string.Empty;
    }

    public class SMSData
    {
        [JsonProperty("@UDH")]
        public string Udh { get; set; } = string.Empty;
        [JsonProperty("@CODING")]
        public string Coding { get; set; } = string.Empty;
        [JsonProperty("@TEXT")]
        public string Text { get; set; } = string.Empty;
        [JsonProperty("@PROPERTY")]
        public string Property { get; set; } = string.Empty;
        [JsonProperty("@ID")]
        public string Id { get; set; } = string.Empty;
        [JsonProperty("ADDRESS")]
        public List<AddressData> Address { get; set; }
    }

    public class AddressData
    {
        [JsonProperty("@FROM")]
        public string From { get; set; } = string.Empty;
        [JsonProperty("@TO")]
        public string To { get; set; } = string.Empty;
        [JsonProperty("@SEQ")]
        public string Seq { get; set; } = string.Empty;
        [JsonProperty("@TAG")]
        public string Tag { get; set; } = string.Empty;
    }

    public class SMSRequestModel
    {
        public string? isBinary { get; set; }
        public int? Coding { get; set; }
        public int? ID { get; set; }
        public string? Text { get; set; }
        public int? Sequence { get; set; }
        public string? ToUser { get; set; }
    }
}
