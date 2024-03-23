/*
 * BSD 3-Clause License
 *
 * Copyright (c) 2024
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its
 *    contributors may be used to endorse or promote products derived from
 *    this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 * OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
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
