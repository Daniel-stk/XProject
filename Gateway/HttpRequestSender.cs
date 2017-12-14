using Responses;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;
using DTO.Tools;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Web;
using Responses.Tools;
using static Responses.Tools.ResponseExtensions;

namespace Gateway
{
    internal class HttpRequestSender
    {
        private static readonly Lazy<HttpRequestSender> _instance = new Lazy<HttpRequestSender>(() => new HttpRequestSender());
        private static HttpClient _httpClient;

        private HttpRequestSender()
        {
            //WebRequestHandler handler = new WebRequestHandler();
            //X509Certificate2 certificate = GetMyX509Certificate();
            //handler.ClientCertificates.Add(certificate);

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(CarotoSettings.Default.BaseAddress);
        }

        public static HttpRequestSender Instance { get { return _instance.Value; } }

        public async Task<Response> GetAsync<T>(string endpoint)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var httpResponse = await _httpClient.GetAsync(endpoint);
            if (httpResponse.IsSuccessStatusCode)
            {
                var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<ComplexResponse<T>>(httpResponseContent);
                return response;
            }
            return null;
        }

        public async Task<Response> GetAsync<T>(string endpoint, Dto payload, ResponseType type)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var uri = BuildUriWithParameters(endpoint, payload);
            var httpResponse = await _httpClient.GetAsync(uri);
            if (httpResponse.IsSuccessStatusCode)
            {
                var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();
                if(type == ResponseType.simple)
                {
                    try
                    { 
                        var response = JsonConvert.DeserializeObject<SimpleResponse<T>>(httpResponseContent);
                        return response;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                {
                    try
                    { 
                        var response = JsonConvert.DeserializeObject<ComplexResponse<T>>(httpResponseContent);
                        return response;
                    }
                    catch(Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return null;
        }

        public async Task<Response> PostAsync(string endpoint, Dto payload)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var encodedPayload = EncodeContent(payload);
            var httpResponse = await _httpClient.PostAsync(endpoint, encodedPayload);
            if (httpResponse.IsSuccessStatusCode)
            {
                var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response>(httpResponseContent);
                return response;
            }
            return null;
        }

        public async Task<Response> PutAsync(string endpoint,Dto payload)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var encodedPayload = EncodeContent(payload);
            var httpResponse = await _httpClient.PutAsync(endpoint, encodedPayload);
            if (httpResponse.IsSuccessStatusCode)
            {
                var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response>(httpResponseContent);
                return response;
            }
            return null;
        }

        public async Task<Response> DeleteAsync(string endpoint, Dto payload)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            var encodedPayload = EncodeContent(payload);
            var httpResponse = await _httpClient.PostAsync(endpoint, encodedPayload);
            if (httpResponse.IsSuccessStatusCode)
            {
                var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<Response>(httpResponseContent);
                return response;
            }
            return null;
        }

        private FormUrlEncodedContent EncodeContent(Dto payload)
        {
            var content = new Dictionary<string, string>();
            var type = payload.GetType();
            foreach(var property in type.GetProperties())
            {
                foreach(var customAttribute in property.GetCustomAttributes(true))
                {
                   if(customAttribute is UrlPropertyNameAttribute)
                    {
                        var attribute = customAttribute as UrlPropertyNameAttribute;
                        var value = property.GetValue(payload);
                        content.Add(attribute.Property, value.ToString());
                    }
                }
            }
            return new FormUrlEncodedContent(content);
        }

        private string BuildUriWithParameters(string endpoint,Dto payload)
        {
            var builder = new UriBuilder(CarotoSettings.Default.BaseAddress + endpoint);
            builder.Port = -1;
            var stringRequest = HttpUtility.ParseQueryString(builder.Query);

            var type = payload.GetType();
            foreach(var property in type.GetProperties())
            {
                foreach(var customAttribute in property.GetCustomAttributes(true))
                {
                    if(customAttribute is UrlPropertyNameAttribute)
                    {
                        var attribute = customAttribute as UrlPropertyNameAttribute;
                        var value = property.GetValue(payload);
                        stringRequest[attribute.Property] = value.ToString();
                    }
                }
            }
            builder.Query = stringRequest.ToString();
            return builder.ToString();
        }
    }
}
