using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;

namespace DemoFunctionApp.Functions;

public class HelloFunction
{
    [Function("Hello")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
    {
        var name = System.Web.HttpUtility.ParseQueryString(req.Url.Query)["name"] ?? "world";
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync($"Hello, {name}!");
        return response;
    }
}
