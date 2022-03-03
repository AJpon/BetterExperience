using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NvNodeApi
{
    public static class Client
    {
        static readonly HttpClient HttpClient;
        private const string MEDIA_TYPE = "application/json";
        public static uint ServerPort { get; set; } = 8080;
        public static string SecurityToken { get; set; } = "";

        static Client()
        {
            HttpClient = new HttpClient();
        }

        public static async Task<HttpResponseMessage> Post(string Path, JObject Content) {
            string json = JsonConvert.SerializeObject(Content, Formatting.Indented);

            // タイムアウト時間の設定(5秒)
            // HttpClient.Timeout = TimeSpan.FromMilliseconds(5000);
            StringContent content = new StringContent(json, Encoding.UTF8, MEDIA_TYPE);
            content.Headers.Add("X_LOCAL_SECURITY_COOKIE", SecurityToken);
            try
            {
                return await HttpClient.PostAsync("http://localhost:"+ServerPort+Path, content);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
        }

        public static async Task<HttpResponseMessage> Get(string Path)
        {
            // タイムアウト時間の設定(5秒)
            // HttpClient.Timeout = TimeSpan.FromMilliseconds(5000);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:" + ServerPort + Path);
            requestMessage.Headers.Add("X_LOCAL_SECURITY_COOKIE", SecurityToken);
            try
            {
                return await HttpClient.SendAsync(requestMessage);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }

        }
            
    }
}
