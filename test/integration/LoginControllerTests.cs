namespace IO.Curity.OAuthAgent.Test
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using Xunit;
    using IO.Curity.OAuthAgent.Entities;

    /*
     * Test the login controller operations
     */
    [Collection("default")]
    public class LoginControllerTests
    {
        private readonly IntegrationTestsState state;
        private readonly string baseUrl;

        public LoginControllerTests(IntegrationTestsState state)
        {
            this.state = state;
            this.baseUrl = "http://localhost:8080/oauth-agent";
        }

        [Fact]
        public async Task LoginController_StartLoginForValidOrigin_ReturnsAuthorizationRequestUrl()
        {
            var url = $"{this.baseUrl}/login/start";
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("origin", "http://www.example.local");
                
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadFromJsonAsync<StartAuthorizationResponse>();
                Assert.True(data.AuthorizationRequestUrl.Length > 0);
            }
        }
    }
}
