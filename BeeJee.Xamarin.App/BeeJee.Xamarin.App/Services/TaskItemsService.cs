using BeeJee.Xamarin.App.Common;
using BeeJee.Xamarin.App.Enums;
using BeeJee.Xamarin.App.Models;
using BeeJee.Xamarin.App.Models.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BeeJee.Xamarin.App.Services
{
    public class TaskItemsService : BaseApiService, ITaskItemsService
    {
        public async Task<ServiceResult<TaskItem>> CreateAsync(NewTaskItem newItem)
        {
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder($"{API_URL}Task/create");

                var postContent = new StringContent(JsonConvert.SerializeObject(newItem), System.Text.Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync(uriBuilder.Uri, postContent).ConfigureAwait(false);

                    var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var parsedObject = JObject.Parse(content);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, TaskItem>))))
                        {
                            var result = JsonConvert.DeserializeObject<ResponseMessage<string, TaskItem>>(content,
                                new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                            return new ServiceResult<TaskItem>()
                            {
                                Status = ResultStatus.Ok,
                                Data = result.Message
                            };
                        }

                        return new ServiceResult<TaskItem>()
                        {
                            Status = ResultStatus.Ok,
                            Data = null
                        };
                    }

                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, Models.ValidationError[]>))))
                        {
                            var responseResult = JsonConvert.DeserializeObject<ResponseMessage<string, Models.ValidationError[]>>(content,
                            new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                            return new ServiceResult<TaskItem>()
                            {
                                Status = ResultStatus.Error,
                                ValidationErrors = responseResult.Message
                            };
                        }

                        if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, string>))))
                        {
                            var responseResult = JsonConvert.DeserializeObject<ResponseMessage<string, string>>(content,
                            new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                            return new ServiceResult<TaskItem>()
                            {
                                Status = ResultStatus.Error,
                                ErrorMessage = responseResult.Message
                            };
                        }
                    }

                    return new ServiceResult<TaskItem>()
                    {
                        Status = ResultStatus.Error,
                        ErrorMessage = "Unknown error"
                    };
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }

        public async Task<ServiceResult<TaskItem>> EditAsync(string text, TaskItemStatus status, int id)
        {
            var token = await SecureStorage.GetAsync(StoreKeys.TOKEN);
            if (token == null)
            {
                return new ServiceResult<TaskItem>()
                {
                    Status = ResultStatus.Error,
                    ErrorMessage = "Unauthorized"
                };

            }

            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder($"{API_URL}Task/edit/{id}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var parameters = new Dictionary<string, string>
                                {
                                    { "text", text },
                                    { "status", ((int)status).ToString() }
                                };

                var postContent = new FormUrlEncodedContent(parameters);
                uriBuilder.Query = await postContent.ReadAsStringAsync();

                var response = await client.PostAsync(uriBuilder.Uri, new StringContent("")).ConfigureAwait(false);

                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var parsedObject = JObject.Parse(content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, TaskItem>))))
                    {
                        var result = JsonConvert.DeserializeObject<ResponseMessage<string, TaskItem>>(content,
                            new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TaskItem>()
                        {
                            Status = ResultStatus.Ok,
                            Data = result.Message
                        };
                    }

                    return new ServiceResult<TaskItem>()
                    {
                        Status = ResultStatus.Ok,
                        Data = null
                    };
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, Models.ValidationError[]>))))
                    {
                        var responseResult = JsonConvert.DeserializeObject<ResponseMessage<string, Models.ValidationError[]>>(content,
                        new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TaskItem>()
                        {
                            Status = ResultStatus.Error,
                            ValidationErrors = responseResult.Message
                        };
                    }

                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, string>))))
                    {
                        var responseResult = JsonConvert.DeserializeObject<ResponseMessage<string, string>>(content,
                        new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TaskItem>()
                        {
                            Status = ResultStatus.Error,
                            ErrorMessage = responseResult.Message
                        };
                    }
                }

                return new ServiceResult<TaskItem>()
                {
                    Status = ResultStatus.Error,
                    ErrorMessage = "Unknown error"
                };
            }
        }

        public async Task<ServiceResult<TaskItemsResponse>> GetAsync(string sort_field, SortDirection sort_direction, int page, int pageSize)
        {
            using (var client = new HttpClient())
            {
                var uriBuilder = new UriBuilder($"{API_URL}Task");
                var sortDirection = sort_direction == SortDirection.ASC ? "asc" : "desc";
                uriBuilder.Query = $"sort_field={sort_field}&sort_direction={sortDirection}&page={page}&pageSize={pageSize}";

                var response = await client.GetAsync(uriBuilder.Uri).ConfigureAwait(false);
                var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var parsedObject = JObject.Parse(content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, TaskItemsResponse>))))
                    {
                        var result = JsonConvert.DeserializeObject<ResponseMessage<string, TaskItemsResponse>>(content,
                            new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TaskItemsResponse>()
                        {
                            Status = ResultStatus.Ok,
                            Data = result.Message
                        };
                    }

                    return new ServiceResult<TaskItemsResponse>()
                    {
                        Status = ResultStatus.Ok,
                    };
                }

                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    if (parsedObject.IsValid(_jSchema.GetJSchema(typeof(ResponseMessage<string, string>))))
                    {
                        var result = JsonConvert.DeserializeObject<ResponseMessage<string, string>>(content,
                        new JsonSerializerSettings() { MissingMemberHandling = MissingMemberHandling.Ignore });
                        return new ServiceResult<TaskItemsResponse>()
                        {
                            Status = ResultStatus.Error,
                            ErrorMessage = result.Message
                        };
                    }
                }

                return new ServiceResult<TaskItemsResponse>()
                {
                    Status = ResultStatus.Error,
                    ErrorMessage = "Unknown error"
                };
            }
        }
    }
}

