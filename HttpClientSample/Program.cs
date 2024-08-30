using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClientSample
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessMessages(10).GetAwaiter().GetResult();

        }


        static async Task ProcessMessages(int loopCnt)
        {
            HttpClient c = new HttpClient();

            for (int i = 0; i < loopCnt; i++)
            {
                HttpResponseMessage r = await c.GetAsync("https://randomuser.me/api/");
                string randomText = await r.Content.ReadAsStringAsync();

                File.AppendAllText("c:\\temp\\RandomText.txt", randomText + Environment.NewLine);

                string JsonText = ParseRandomText(randomText);
                File.AppendAllText("c:\\temp\\JsonText.txt", JsonText + Environment.NewLine);


            }
        }

        public static async Task<string> GetRandomAPIData(HttpClient c, string randomUserURL)
        {
            HttpResponseMessage r = await c.GetAsync(randomUserURL);
            string rb = await r.Content.ReadAsStringAsync();
            return rb;
        }


        public static string ParseRandomText(string randomTxt)
        {
            try
            {
                JObject o = JObject.Parse(randomTxt);

                string lastName = o["results"][0]["name"]["last"].ToString();
                string firstName = o["results"][0]["name"]["first"].ToString();
                string city = o["results"][0]["location"]["city"].ToString();
                string email = o["results"][0]["email"].ToString();
                string age = o["results"][0]["dob"]["age"].ToString();

                var json = "{\"last\":" + "\"" + lastName + "\"" + ",\"first\":" + "\"" + firstName + "\"" + ",\"city\":" + "\"" + city + "\"" + ",\"email\":" + "\"" + email + "\"" + ",\"age\":" + "\"" + age + "\"" + "}";
                return json;
            }
            catch (Exception ex)
            {
                throw new Exception($"Unexpected format on random string ({randomTxt})");
            }

        }
    }
}
