using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace DesafioServiceNetAPI.Facebook
{
    public class FaceBookHandler
    {
        private FaceBookSettings settings;

        public FaceBookHandler(IOptions<FaceBookSettings> fbSettings)
        {
            settings = fbSettings.Value;
            settings.tokenAppUrl =  settings.tokenAppUrl.Replace("APP_ID", settings.AppId).Replace("APP_SECRET", settings.AppSecret);
        }

        public async Task<bool> ValidToken(string token)
        {
            using(HttpClient httpClient = new HttpClient())
            {
                try 
                {
                    var response = await httpClient.GetAsync(settings.tokenAppUrl);
                    var result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var tokenAceso = JsonConvert.DeserializeObject<TokenAcesso>(result);
                    response.EnsureSuccessStatusCode();
                    
                    var validTokenUrl = settings.tokenValidationUrl.Replace("TOKENINSPECT", token).Replace("APPTOKEN", tokenAceso.access_token);
                    response = await httpClient.GetAsync(validTokenUrl);
                    result = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    response.EnsureSuccessStatusCode();
                } 
                catch(HttpRequestException e)
                {
                    return false;
                }

                return true;
            }
        }
    }
}
