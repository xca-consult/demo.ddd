using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Demo.DDD.ApplicationServices.ReadModels;
using Demo.DDD.Requests;

namespace Demo.DDD.SystemTests
{
    public class UserApi
    {
        public async Task<UserDetailsReadModel> GetUserDetails(HttpClient client, Guid id)
        {
            var httpResponseMessage = await client.GetAsync("/api/user/" + id + "/details");
            var readModel = JsonConvert.DeserializeObject<UserDetailsReadModel>(await httpResponseMessage.Content.ReadAsStringAsync());
            httpResponseMessage.EnsureSuccessStatusCode();
            return readModel;
        }

        public async Task<Guid> CreateUser(HttpClient client, string name)
        {
            var createUserRequest = new CreateUserRequest { Name = name };
            HttpContent postContent = new StringContent(JsonConvert.SerializeObject(createUserRequest), Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PostAsync("/api/user", postContent);
            var postResponse = JsonConvert.DeserializeObject<PostResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            httpResponseMessage.EnsureSuccessStatusCode();
            return postResponse.Id;
        }
        public async Task<HttpStatusCode> UpdatePhone(HttpClient client, Guid id, string countryCode, String phone)
        {
            var updatePhoneRequest = new UpdatePhoneNumberRequest { CountryCode = countryCode, PhoneNumber = phone };
            HttpContent patchContent = new StringContent(JsonConvert.SerializeObject(updatePhoneRequest), Encoding.UTF8, "application/json");
            var httpResponseMessage = await client.PatchAsync("/api/user/" + id, patchContent);
            return httpResponseMessage.StatusCode;
        }

        private class PostResponse
        {
            public Guid Id { get; set; }
        }
    }
}
