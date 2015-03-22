using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using WindowsPhone_TokenBasedAuth.Models;
using WindowsPhone_TokenBasedAuth.Services;
using Newtonsoft.Json;


namespace WindowsPhone_TokenBasedAuth.Services
{
    public class TokenService : ITokenService
    {

        /// <summary>
        /// TR: Diskte -Local Storage- token bilgisinin bulunup bulunmadığını kontrol eder.
        /// </summary>
        /// <returns>Token diskte var ise true döndürür.</returns>
        public bool IsTokenExist()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("token"))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        /// <summary>
        /// Diskte varolan tokenı döndürür.
        /// </summary>
        /// <returns>Diskte varolan token nesnesini döndürür.</returns>
        public Token GetToken()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
           
            //token diskte json olarak tututluyor.
            string tokenJsonData = "";

            try
            {
                tokenJsonData = localSettings.Values["token"].ToString();
            }
            catch (Exception)
            {
                throw;
            }

            return JsonConvert.DeserializeObject<Token>(tokenJsonData);

        }

        /// <summary>
        /// Parametre olarak aldığı token'ın geçerlilik süresini kontrol eder.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>Token geçerliyse true döndürür.</returns>
        public bool IsValidToken(Token token)
        {
            DateTime expires = DateTime.Now.AddSeconds(Convert.ToDouble(token.ExpiresIn));

            if (DateTime.Now > expires)
                return false;

            return true;
        }


        /// <summary>
        /// Diske token JSON formatında kaydeder.
        /// </summary>
        /// <param name="token">Kaydedilecek token.</param>
        /// <returns>Kaydetme işlemi başarıyla sonuçlanırsa true döner.</returns>
        public bool SaveToken(Token token)
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string tokenJsonData = "";

            tokenJsonData = JsonConvert.SerializeObject(token);

            try
            {
                localSettings.Values["token"] = tokenJsonData;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
            return true;
        }


        /// <summary>
        /// Diskte varolan tokenı siler.
        /// </summary>
        /// <returns>Silme işlemi başarıyla gerçekleşirse true döner.</returns>
        public bool DeleteToken()
        {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            try
            {
                localSettings.Values.Remove("token");
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }

        
    }
}
