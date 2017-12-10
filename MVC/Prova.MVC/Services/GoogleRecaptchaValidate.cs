using Newtonsoft.Json.Linq;
using System.Net;

namespace Prova.MVC.Services
{
    public class GoogleRecaptchaValidate
    {
        public static bool Validate(string response)
        {            
            var secretKey = "6LfRZzwUAAAAAEy5wLloXsAnKuVEih11WiGmvkZ_";

            using (var client = new WebClient())
            {
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");
                return status;
            }
        }
    }
}