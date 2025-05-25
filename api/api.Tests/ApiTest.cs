namespace api.Tests;
using sposko;
using LinqToDB;
using LinqToDB.Data;

public class ApiTest
{
    const string apiUrl = "http://localhost:5023/api/";

    [Fact]
    public async Task ApiConnectionTest()
    {
        var httpClient = new HttpClient();
        var exception = await Record.ExceptionAsync(() => httpClient.GetAsync($"{apiUrl} sportgroup"));
        Assert.Null(exception);
    }
}

