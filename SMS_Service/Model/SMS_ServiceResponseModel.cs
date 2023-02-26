namespace SMS_Service.Model
{
    public class SMS_ServiceResponseModel
    {
        public MessageBackData MESSAGEACK { get; set; } 
    }

    public class MessageBackData
    {
        public GUIDData GUID { get; set; }
        public ERRORData Err { get; set; }
    }

    public class GUIDData
    {
        public DateTime? SUBMITDATE { get; set; }
        public string GUID { get; set; }
        public ERRORData ERROR { get; set; }
        public int? ID { get; set; }
    }

    public class ERRORData
    {
        public int? Code { get; set; }
        public int? SEQ { get; set; }
        public string? Desc { get; set; }
    }
}
