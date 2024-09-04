using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebApiClient
{
    public class WebClient<T> : IWebClient<T>
    {

        string server;
        string controller;
        string method;
        Dictionary<string, string> keyValues;

        HttpClient client;
        HttpRequestMessage requestMessage;
        HttpResponseMessage responseMessage;

        public WebClient(string server, string controller,
               string method)
        {
            this.server = server;
            this.controller = controller;
            this.method = method;
            this.keyValues = new Dictionary<string, string>();
            this.client = new HttpClient();
            this.requestMessage = new HttpRequestMessage();
        }

        public void AddNewKeyValues(string key, string value)
        {
            this.keyValues.Add(key, value);
        }
        public void ClearKeyValues()
        {
            this.keyValues.Clear();
        }
        public T Get()
        {
            try
            {
                this.requestMessage.Method = HttpMethod.Get;
                string uri = $"http://{this.server}/api/{this.controller}/{this.method}";

                if (this.keyValues.Count() > 0)
                {
                    var queryParams = new List<string>();
                    foreach (KeyValuePair<string, string> parameter in this.keyValues)
                    {
                        queryParams.Add($"{parameter.Key}={Uri.EscapeDataString(parameter.Value)}");
                    }
                    uri += "?" + string.Join("&", queryParams);
                }

                Console.WriteLine($"Request URI: {uri}"); // Log the full request URI

                this.requestMessage.RequestUri = new Uri(uri);
                this.responseMessage = this.client.SendAsync(this.requestMessage).Result;

                Console.WriteLine($"Response Status Code: {this.responseMessage.StatusCode}"); // Log response status code
                Console.WriteLine($"Response Reason Phrase: {this.responseMessage.ReasonPhrase}"); // Log response reason phrase
                Console.WriteLine($"Response Content: {this.responseMessage.Content.ReadAsStringAsync().Result}"); // Log response content

                if (this.responseMessage.IsSuccessStatusCode)
                {
                    return this.responseMessage.Content.ReadAsAsync<T>().Result;
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }

            return default(T);
        }

        public async Task<T> GetAsync()
        {
            this.requestMessage.Method = HttpMethod.Get;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            if (this.keyValues.Count > 0)
            {
                uri = uri + "/?";
                foreach (KeyValuePair<string, string> keyValue in this.keyValues)
                {
                    uri = uri + keyValue.Key + "=" + keyValue.Value + "&";
                }
            }
            this.requestMessage.RequestUri = new Uri(uri);
            this.responseMessage = await this.client.SendAsync(this.requestMessage);
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return await this.responseMessage.Content.ReadAsAsync<T>();
            }
            return default(T);
        }

        public bool Post(string id)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            this.requestMessage.Content = new StringContent(id, Encoding.UTF8, "application/json");
            this.responseMessage = this.client.SendAsync(this.requestMessage).Result;
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return this.responseMessage.Content.ReadAsAsync<bool>().Result;
            }
            return false;
        }


        public bool Post(T model)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            this.requestMessage.Content = objectContent;
            this.responseMessage = this.client.SendAsync(this.requestMessage).Result;
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return this.responseMessage.Content.ReadAsAsync<bool>().Result;
            }
            return false;
        }


        public bool Post(T model, Stream file)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            StreamContent streamContent = new StreamContent(file);
            multipartFormDataContent.Add(objectContent, "model");
            multipartFormDataContent.Add(streamContent, "file", "file");
            this.requestMessage.Content = multipartFormDataContent;
            this.responseMessage = this.client.SendAsync(this.requestMessage).Result;
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return this.responseMessage.Content.ReadAsAsync<bool>().Result;
            }
            return false;
        }

        public bool Post(T model, List<string> files)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            multipartFormDataContent.Add(objectContent, "model");
            foreach (string file in files)
            {
                StreamContent streamContent = new StreamContent(File.OpenRead(file));
                multipartFormDataContent.Add(streamContent, "file");
            }
            this.requestMessage.Content = multipartFormDataContent;
            this.responseMessage = this.client.SendAsync(this.requestMessage).Result;
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return this.responseMessage.Content.ReadAsAsync<bool>().Result;
            }
            return false;
        }

        public Task<bool> PostAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> PostAsync(T model)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            this.requestMessage.Content = objectContent;
            this.responseMessage = await this.client.SendAsync(this.requestMessage);
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return await this.responseMessage.Content.ReadAsAsync<bool>();
            }
            return false;
        }

        public async Task<bool> PostAsync(T model, string file)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            StreamContent streamContent = new StreamContent(File.OpenRead(file));
            multipartFormDataContent.Add(objectContent, "model");
            multipartFormDataContent.Add(streamContent, "file");
            this.requestMessage.Content = multipartFormDataContent;
            this.responseMessage = await this.client.SendAsync(this.requestMessage);
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return await this.responseMessage.Content.ReadAsAsync<bool>();
            }
            return false;
        }

        public async Task<bool> PostAsync(T model, List<string> files)
        {
            this.requestMessage.Method = HttpMethod.Post;
            string uri = $"http://{this.server}/api/{this.controller}/{this.method}";
            this.requestMessage.RequestUri = new Uri(uri);
            MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
            ObjectContent<T> objectContent = new ObjectContent<T>(model, new JsonMediaTypeFormatter());
            multipartFormDataContent.Add(objectContent, "model");
            foreach (string file in files)
            {
                StreamContent streamContent = new StreamContent(File.OpenRead(file));
                multipartFormDataContent.Add(streamContent, "file");
            }
            this.requestMessage.Content = multipartFormDataContent;
            this.responseMessage = await this.client.SendAsync(this.requestMessage);
            if (this.responseMessage.IsSuccessStatusCode == true)
            {
                return await this.responseMessage.Content.ReadAsAsync<bool>();
            }
            return false;
        }
    }
}
