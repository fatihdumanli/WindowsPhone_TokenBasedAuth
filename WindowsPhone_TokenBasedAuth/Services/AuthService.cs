using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WindowsPhone_TokenBasedAuth.Models;
using Newtonsoft.Json;

namespace WindowsPhone_TokenBasedAuth.Services
{
    public class AuthService : IAuthService
    {

        private const string baseUri = "http://localhost:26296";

        /// <summary>
        /// Authorization Server'dan Bearer Token'ı alır.
        /// </summary>
        /// <param name="username">Kullanıcı adı(e-mail)</param>
        /// <param name="password">Parola</param>
        /// <returns>Server tarafından verilen Access Token'ı döndürür.</returns>
        public async Task<Token> GetAccessToken(string username, string password)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post,
                    String.Format("{0}/token", baseUri));

                req.Content = new StringContent(
                    String.Format("grant_type=password&username={0}&password={1}",
                    username, password), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await httpClient.SendAsync(req);
                var result = await response.Content.ReadAsStringAsync();
                var token = JsonConvert.DeserializeObject<Token>(result);

                return token;
            }
        }

        public async Task<bool> Logout(Token token)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUri);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post,
                    String.Format("{0}/api/account/logout", baseUri));


                req.Headers.Add("Authorization", String.Format("Bearer {0}", token.AccessToken));

                var response = await httpClient.SendAsync(req);
                var result = await response.Content.ReadAsStringAsync();

                return true;
            }
        }
    }
}
