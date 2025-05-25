namespace api.Tests;
using sposko;
using LinqToDB;
using LinqToDB.Data;

public class DbTest
{
    [Fact]
    public void DbConnectionTest()
    {
        var exception = Record.Exception(() =>
          {
              var dataOptions = new DataOptions<SposkoDb>(
                new DataOptions()
                .UsePostgreSQL("Host=localhost;Username=user;Password=password;Database=sposko;IncludeErrorDetail=true")
            );
              new SposkoDb(dataOptions);

          });
        Assert.Null(exception);
    }
}
