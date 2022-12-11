using BeeJee.Xamarin.App.Enums;
using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Auth;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BeeJee.Xamarin.App.Services
{
    public class TokenService : BaseApiService, ITokenService
    {
        public async Task<ServiceResult<TokenResponse>> GetToken(Credentials credentials)
        {
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder($"{API_URL}Auth/login");

                var postContent = new StringContent(JsonConvert.SerializeObject(credentials), System.Text.Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uriBuilder.Uri, postContent).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var parsedObject = JObject.Parse(content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(TokenResponse))))
                    {
                        var result = JsonConvert.DeserializeObject<TokenResponse>(content,
                            new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TokenResponse>()
                        {
                            Status = ResultStatus.Ok,
                            Data = result
                        };
                    }
                }

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ServiceResult<TokenResponse>()
                    {
                        Status = ResultStatus.Error,
                        ErrorMessage = "Неверный логин или пароль"
                    };
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, Models.ValidationError[]>))))
                    {
                        var responseResult = JsonConvert.DeserializeObject<ResponseMessage<string, Models.ValidationError[]>>(content,
                        new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TokenResponse>()
                        {
                            Status = ResultStatus.Error,
                            ValidationErrors = responseResult.Message
                        };
                    }

                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, string>))))
                    {
                        var responseResult = JsonConvert.DeserializeObject<ResponseMessage<string, string>>(content,
                        new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TokenResponse>()
                        {
                            Status = ResultStatus.Error,
                            ErrorMessage = responseResult.Message
                        };
                    }
                }

                return new ServiceResult<TokenResponse>()
                {
                    Status = ResultStatus.Error,
                    ErrorMessage = "Unknown error"
                };
            }
        }
    }
}
