using Kadabra.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Collections.Generic;
using System;
using Newtonsoft.Json.Linq;

namespace Kadabra.Services
{
    public class ApiServices
    {
        
        public async Task<bool> RegisterAsync(string email, string password, string confirmPassword)
        {
            var client = new HttpClient();

            var model = new RegisterBindingModel {
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var json = JsonConvert.SerializeObject(model);

            HttpContent content = new StringContent(json);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync("http://localhost:55284/api/Account/Register", content);

            return response.IsSuccessStatusCode;

        }        

        public async Task<string> LoginAsync(string userName, string password)
        {

            var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("userName", userName),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:55284/Token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var jwt = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(jwt);

            var accessToken = jwtDynamic.Value<string>("access_token");

            Debug.WriteLine(jwt);

            return accessToken;

            /*
            try
            {
                 if(this._HttpClient.DefaultRequestHeaders.Authorization == null)
                {
                    var keyValues = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("userName", userName),
                        new KeyValuePair<string, string>("password", password),
                        new KeyValuePair<string, string>("grant_type", "password")
                    };

                    using (var _response = await this._HttpClient.PostAsync("http://localhost:55284/Token", new FormUrlEncodedContent(keyValues)))
                    {
                        if (!_response.IsSuccessStatusCode)
                            throw new InvalidCastException("Conexão lenta ou Senha e email, podem estar errados");
                        var _result = await _response.Content.ReadAsStringAsync();
                        return _response.IsSuccessStatusCode;
                    }
                }
            }
            catch (Exception)
            {
                throw; 
            }
            return false;
            */
        }

    }
}
