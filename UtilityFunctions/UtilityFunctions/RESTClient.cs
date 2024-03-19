using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UtilityFunctions
{
    public class RESTClient
    {
        private readonly string BaseAddress;

        public RESTClient(string BaseAddress)
        {
            this.BaseAddress = BaseAddress;
        }

        public async Task<T> Get<T>(string api, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            using (HttpClient client = new HttpClient())
            {

                try
                {
                    DefaultHttpClientSetup(client);

                    string queryString = BuildQueryString(api, parameters);

                    HttpResponseMessage response = await client.GetAsync(queryString, HttpCompletionOption.ResponseContentRead);
                    if (response.IsSuccessStatusCode)
                    {
                      var json = await response.Content.ReadAsStringAsync();
                      T DeserializedResource = JsonConvert.DeserializeObject<T>(json);

                      return DeserializedResource;
                    }
   
                }
                catch (Exception ex)
                {

                }

            }

            return default(T);
        }

        private void DefaultHttpClientSetup(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.BaseAddress = new Uri(BaseAddress);
        }

        private string BuildQueryString(string api, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            string queryString = api;

            if (parameters != null)
            {
                bool IsFirst = true;
                foreach (var kv in parameters)
                {
                    if (IsFirst)
                    {
                        IsFirst = false;
                        queryString += string.Format($"?{kv.Key}={ kv.Value}");
                    }
                    else
                    {
                        queryString += string.Format($"&{kv.Key}={ kv.Value}");
                    }

                }
            }

            return queryString;
        }
    }
  
}
