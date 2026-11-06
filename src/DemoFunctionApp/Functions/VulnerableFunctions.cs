using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Data.SqlClient;
using System.Net;

namespace DemoFunctionApp.Functions;

// DEMO ONLY: intentionally vulnerable code so CodeQL has findings to show.
// Do not copy into real projects.
public class VulnerableFunctions
{
    // Hardcoded credential (CodeQL: cs/hardcoded-credentials)
    private const string ConnectionString =
        "Server=tcp:demo.database.windows.net;Database=demo;User Id=admin;Password=P@ssw0rd123!;";

    // SQL injection (CodeQL: cs/sql-injection)
    [Function("GetUser")]
    public async Task<HttpResponseData> GetUser(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var id = System.Web.HttpUtility.ParseQueryString(req.Url.Query)["id"];
        var response = req.CreateResponse(HttpStatusCode.OK);

        using var conn = new SqlConnection(ConnectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand("SELECT * FROM Users WHERE Id = '" + id + "'", conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            await response.WriteStringAsync(reader["Name"].ToString() ?? string.Empty);
        }
        return response;
    }

    // Path traversal (CodeQL: cs/path-injection)
    [Function("ReadFile")]
    public async Task<HttpResponseData> ReadFile(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        var file = System.Web.HttpUtility.ParseQueryString(req.Url.Query)["file"] ?? "";
        var content = await File.ReadAllTextAsync(Path.Combine("data", file));
        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteStringAsync(content);
        return response;
    }

    // Weak hashing (CodeQL: cs/weak-hashing / insecure algorithm)
    public static string HashPassword(string password)
    {
        using var md5 = System.Security.Cryptography.MD5.Create();
        return Convert.ToHexString(md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
    }
}
