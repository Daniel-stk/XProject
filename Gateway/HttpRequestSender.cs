using Responses;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DTO;
using DTO.Tools;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Gateway
{
    internal class HttpRequestSender
    {
        private static HttpClient _httpClient = new HttpClient();

        public HttpRequestSender()
        {
            _httpClient.BaseAddress = new Uri(Caroto.CarotoSettings.Default.BaseAddress);
        }

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

        public async Task<Response> GetAsync<T>(string endpoint, Dto payload)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var encodedPayload = EncodeContent(payload);
            var uri = new Uri(_httpClient.BaseAddress + endpoint + encodedPayload.ToString());
            var httpResponse = await _httpClient.GetAsync(uri);
            if (httpResponse.IsSuccessStatusCode)
            {
                var httpResponseContent = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<SimpleResponse<T>>(httpResponseContent);
                return response;
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
                        content.Add(attribute.Property, property.GetValue(property).ToString());
                    }
                }
            }
            return new FormUrlEncodedContent(content);
        }
    }
}
