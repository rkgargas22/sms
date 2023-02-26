using SMS_Service.Common;
using SMS_Service.Model;

namespace SMS_Service.Services.Contracts
{
    public interface ISMS_Interface
    {
        Task<ServiceResponseModel> SendSMSService(SMSRequestModel requestModel);

        Task<ServiceResponseModel> GetTemplateDataByModule(string Module);
    }
}
