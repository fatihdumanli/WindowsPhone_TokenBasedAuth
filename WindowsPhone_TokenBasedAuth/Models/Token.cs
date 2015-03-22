using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


/*
 * Author: Fatih Dumanlı
 * http://fatihdumanli.com
 * 
 * This documentation was written in Turkish
 */

namespace WindowsPhone_TokenBasedAuth.Models
{
    /// <summary>
    /// Web API'dan dönen token'ı temsil eden sınıf.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Token'ın kendisi.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        
        /// <summary>
        /// Token tipi, 'bearer' dönüyor. burası önemli değil
        /// </summary>
        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        /// <summary>
        /// Token'ın geçerlilik süresi(sn cinsinden)
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }

        /// <summary>
        /// Bu token hangi kullanıcı için(email adresi tutulur)
        /// </summary>
        [JsonProperty("userName")]
        public string Username { get; set; }

        /// <summary>
        /// Token'ın talep edilme tarihi(DateTime)
        /// </summary>
        [JsonProperty(".issued")]
        public string Issued { get; set; }

        /// <summary>
        /// Token'ın geçerliliğinin zaman sona ereceği(DateTime)
        /// </summary>
        [JsonProperty(".expires")]
        public string Expires { get; set; }


    }
}
