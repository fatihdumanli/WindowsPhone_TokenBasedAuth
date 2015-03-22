using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WindowsPhone_TokenBasedAuth.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace WindowsPhone_TokenBasedAuth.Services
{
    public class HelloService
    {
        private const string baseUri = "http://localhost:26296";

        public async Task<string> Get(Token token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

                string result = "";

                try
                {
                    result = await httpClient.GetStringAsync(
                        new Uri(String.Format("{0}/api/hello", baseUri), UriKind.RelativeOrAbsolute));

                }
                catch (Exception ex)
                {
                    throw;
                    //TODO: exception u log edebiliriz
                }

                return result;

            }
        }
    }
}
