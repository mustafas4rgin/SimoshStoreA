using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace SimoshStore;

public class ApiHelper
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ApiHelper(IHttpClientFactory clientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _clientFactory = clientFactory;
        _httpContextAccessor = httpContextAccessor;
    }

    
    public async Task<HttpResponseMessage> SendApiRequestAsync<T>(string endpoint, HttpMethod method, T content) where T : class

    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken"); 
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("Token is missing or expired");
        }

        var client = _clientFactory.CreateClient("Api.Data");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 

        HttpRequestMessage requestMessage = new HttpRequestMessage(method, endpoint);

        if (content != null)
        {
            var jsonContent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            requestMessage.Content = jsonContent; 
        }

        var response = await client.SendAsync(requestMessage);
        return response; 
    }
    public async Task<HttpResponseMessage> SendDeleteRequestAsync(string endpoint, HttpMethod method, int id)
    {
        var token = _httpContextAccessor.HttpContext.Session.GetString("JwtToken"); 
        if (string.IsNullOrEmpty(token))
        {
            throw new UnauthorizedAccessException("Token is missing or expired");
        }

        var client = _clientFactory.CreateClient("Api.Data");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 

        var requestUri = $"{endpoint}/{id}";

        HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);

        var response = await client.SendAsync(requestMessage);
        return response; 
    }

}
