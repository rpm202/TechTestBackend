using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tech_test.Services
{
    public class SoapClientService
    {
        private readonly HttpClient _http;

        private const string SoapEndpoint = "http://isapi.mekashron.com/icu-tech/icutech-test.dll";

        public SoapClientService()
        {
            _http = new HttpClient();
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
               xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
               xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <Login xmlns=""http://tempuri.org/"">
      <ol_EntityID>0</ol_EntityID>
      <ol_Username>{System.Security.SecurityElement.Escape(username)}</ol_Username>
      <ol_Password>{System.Security.SecurityElement.Escape(password)}</ol_Password>
    </Login>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            var response = await _http.PostAsync(SoapEndpoint, content);
            var xml = await response.Content.ReadAsStringAsync();
            return xml;
        }

        public async Task<string> RegisterAsync(string customerJson)
        {
            var soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
               xmlns:xsd=""http://www.w3.org/2001/XMLSchema""
               xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
  <soap:Body>
    <RegisterNewCustomer xmlns=""http://tempuri.org/"">
      <customerJson>{System.Security.SecurityElement.Escape(customerJson)}</customerJson>
    </RegisterNewCustomer>
  </soap:Body>
</soap:Envelope>";

            var content = new StringContent(soapEnvelope, Encoding.UTF8, "text/xml");
            var response = await _http.PostAsync(SoapEndpoint, content);
            var xml = await response.Content.ReadAsStringAsync();
            return xml;
        }
    }
}