using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataModel;
using System.Linq;
using LinqToDB;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class SportsController(SposkoDb db) : ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<Sport> Get()
    {
        var sports = (from s in db.Sports
                      select s).AsAsyncEnumerable();
        await foreach (var sport in sports)
        {
            yield return sport;
        }
    }

    [HttpGet("{id}")]
    public async Task<Sport?> Get(int id)
    {
        return await db.Sports.FindAsync(id);
    }

    [HttpPost]
    public async Task Post([FromBody] Sport value)
    {
        await db.Sports
          .Value(s => s.Id, value.Id)
          .Value(s => s.Name, value.Name)
          .InsertAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] Sport value)
    {
        await db.Sports.Where(s => s.Id == id)
          .Set(s => s.Id, value.Id)
          .Set(s => s.Name, value.Name)
          .UpdateAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        await db.Sports.Select(s => s.Id == id).DeleteAsync();
    }
}
