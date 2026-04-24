using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace AppRpgEtec.Services
{
    public class Request
    {
        //metodo para retornar um int, provavelmente utilizado com o uso do ID vindo do banco
        public async Task<int> PostReturnIntAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }

        //retorna um objeto, desseraliezando ele e trazendo ele normal/ metodo generico
        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = data;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            else
                throw new Exception(serialized);

            return result;
        }

        //método put
        public async Task<int> PutAsync<TResult>(string uri, TResult data, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }

        //método get
        public async Task<TResult> GetAsync<TResult>(string uri, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception(serialized);

            TResult result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            return result;
        }

        //método delete
        public async Task<int> DeleteAsync(string uri, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            HttpResponseMessage response = await httpClient.DeleteAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }
    }
}
