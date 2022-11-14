using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleTranslate
{
    public static class HttpRestClient
    {
        public static async Task<T> GetAsyncMethod<T>(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            var responseMessage = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
            var resultAsync = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultAsync);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static async Task<T> PostAsyncMethod<T, T1>(string url, T1 val)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            var json = JsonConvert.SerializeObject(val);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync(url, content);
            var resultAsync = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultAsync);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static async Task<T> PutAsyncMethod<T, T1>(string url, T1 val)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            var json = JsonConvert.SerializeObject(val);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync(url, content);
            var resultAsync = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultAsync);
            return (T)Convert.ChangeType(result, typeof(T));
        }

        public static async Task<T> DeleteAsyncMethod<T>(string url)
        {
            var client = new HttpClient { BaseAddress = new Uri(url) };
            var responseMessage = await client.DeleteAsync(url);
            var resultAsync = await responseMessage.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(resultAsync);
            return (T)Convert.ChangeType(result, typeof(T));
        }
    }
}
