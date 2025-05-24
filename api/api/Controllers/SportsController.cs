using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public async Task Post([FromBody] SportDTO sport)
    {
        await db.Sports
          .Value(s => s.Name, sport.Name)
          .InsertAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] SportInsert sport)
    {
        await db.Sports.Where(s => s.Id == id)
          .Set(s => s.Name, sport.Name)
          .UpdateAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var query = db.Sports.Where(s => s.Id == id);
        await query.DeleteAsync();
    }
}
