using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Net.Http;

namespace Invillia.ExternalService.App
{
    public class InvilliaService
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly string CONTENT_TYPE_JSON = "application/json; charset=utf-8";
        private static readonly string CONTENT_TYPE_WWW_FORM_URLENCODED = "application/x-www-form-urlencoded";

        private static string _UrlPortal { get; set; }

        public static void InitializeConfig(string urlPortal)
        {
            _UrlPortal = urlPortal;
            _client.DefaultRequestHeaders.Clear();
        }

        public static T ExecuteRequestLogin<T>(string urlOperacao, string userName, string password)
        {

            var url = _UrlPortal + urlOperacao;
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", CONTENT_TYPE_WWW_FORM_URLENCODED);


            request.AddParameter("Username", userName);
            request.AddParameter("Password", password);


            IRestResponse response = client.Execute(request);

            if (response.StatusCode.Equals((HttpStatusCode)200))
            {
                var _serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };
                var teste = JsonConvert.DeserializeObject<T>(response.Content, _serializerSettings);
                return JsonConvert.DeserializeObject<T>(response.Content, _serializerSettings);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
