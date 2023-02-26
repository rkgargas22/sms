using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMS_Service.Common;
using SMS_Service.Model;
using SMS_Service.Services.Contracts;
using System.Net.Http.Headers;

namespace SMS_Service.Services
{
    public class SMS_Services : ISMS_Interface
    {
        private readonly IConfiguration _configuration;

        public SMS_Services(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<ServiceResponseModel> GetTemplateDataByModule(string Module)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseModel> SendSMSService(SMSRequestModel requestModel)
        {
            ServiceResponseModel serviceResponse = new ServiceResponseModel();
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJodHRwczovL2FwaS5teXZhbHVlZmlyc3QuY29tL3BzbXMiLCJzdWIiOiJkZW1vbGVtZWlseG1sIiwiZXhwIjoxNjc0Mjk2OTQyfQ.nx_xC8BV1vYSrCLtA81kH3ZlNbR7oKUvZqv1t5vOf2E");

                    var userName = _configuration.GetSection("SMS_Configuration:username").Value;
                    var password = _configuration.GetSection("SMS_Configuration:password").Value;
                    var provider = _configuration.GetSection("SMS_Configuration:serviceName").Value;

                    var AddDataList = new List<AddressData>();
                    var AddData = new AddressData
                    {
                        From = provider,
                        To = requestModel.ToUser,
                        Seq = Convert.ToString(requestModel.Sequence),
                        Tag = "some clientside random data"
                    };
                    AddDataList.Add(AddData);

                    var SMSList = new List<SMSData>();
                    var SMSData = new SMSData
                    {
                        Udh = requestModel.isBinary,
                        Coding = Convert.ToString(requestModel.Coding),
                        Text = requestModel.Text,
                        Property = "0",
                        Id = Convert.ToString(requestModel.ID),
                        Address = AddDataList
                    };
                    SMSList.Add(SMSData);

                    var Data = new SMS_ServiceRequestModel
                    {
                        Ver = "1.2",
                        Dlr = new DLRData
                        {
                            Url = ""
                        },
                        User = new UserData
                        {
                            UserName = userName,
                            Password = password,
                            UnixTimestamp = ""
                        },
                        Sms = SMSList
                    };

                    var Data1 = JsonConvert.SerializeObject(Data);

                    var response = await client.PostAsJsonAsync("https://api.myvaluefirst.com/psms/servlet/psms.JsonEservice", Data1);
                    //var requestObj = new HttpRequestMessage(HttpMethod.Post, "https://api.myvaluefirst.com/psms/servlet/psms.JsonEservice");

                    //requestObj.Content = JsonContent.Create(Data1);

                    //var response = await client.SendAsync(requestObj);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseObj = await response.Content.ReadAsStringAsync();
                        var responseModel = JsonConvert.DeserializeObject<SMS_ServiceResponseModel>(responseObj);

                        if (responseModel != null && responseModel.MESSAGEACK != null)
                        {
                            if (responseModel.MESSAGEACK.GUID != null && responseModel.MESSAGEACK.GUID.ERROR != null)
                            {
                                serviceResponse = new ServiceResponseModel(responseModel.MESSAGEACK.GUID, true);
                            }
                            else if (responseModel.MESSAGEACK.Err != null)
                            {
                                serviceResponse = new ServiceResponseModel(responseModel.MESSAGEACK.Err.Desc, true);
                            }
                            else
                            {
                                serviceResponse = new ServiceResponseModel(responseModel.MESSAGEACK.GUID, false);
                            }
                        }

                    }
                    else
                    {
                        serviceResponse = new ServiceResponseModel("SMS Response Failure", true);
                    }

                }
            }
            catch (Exception e)
            {
                serviceResponse = new ServiceResponseModel(e);
            }
            return serviceResponse;
        }
    }
}
