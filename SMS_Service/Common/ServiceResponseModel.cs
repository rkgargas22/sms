namespace SMS_Service.Common
{
    public class ServiceResponseModel
    {
        public string status { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public string[] error { get; set; }
        public bool IsSuccess { get; set; }

        public ServiceResponseModel() { }

        public ServiceResponseModel(object response, bool isError)
        {
            status = isError ? Status_Code.ServerError.ToString() : Status_Code.Success.ToString();
            message = isError ? StringEnum.GetStringValue(Message.ServerError) : StringEnum.GetStringValue(Message.Success);
            data = isError ? null : response;
            error = (isError ? (new string[1] { (string)response }) : null);
            IsSuccess = !isError;
        }

        public ServiceResponseModel(Exception e)
        {
            status = Status_Code.ServerError.ToString();
            message = StringEnum.GetStringValue(Message.ServerError);
            data = null;
            error = new string[1] { e.Message };
            IsSuccess = false;
        }
    }

    public enum Status_Code
    {
        Success = 200,
        Redirection = 300,
        Client = 400,
        ServerError = 500
    }

    public enum Message
    {
        [StringValue("Success")]
        Success,
        [StringValue("Failure")]
        Failure,
        [StringValue("Redirection Error")]
        Redirection,
        [StringValue("Client Server Error")]
        Client,
        [StringValue("Internal Server Error")]
        ServerError
    }
}
