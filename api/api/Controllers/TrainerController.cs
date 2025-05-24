using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class TrainerController(ITrainerService trainerService) : ControllerBase
{
    [HttpGet]
    public async IAsyncEnumerable<TrainerDTO> Get()
    {
        var trainers = trainerService.GetTrainers();
        await foreach (var trainer in trainers)
        {
            yield return trainer;
        }
    }

    [HttpGet("{id}")]
    public async Task<TrainerDTO?> Get(Guid id)
    {
        return await trainerService.GetTrainerById(id);
    }

    [HttpPost]
    public async Task<TrainerDTO?> Post([FromBody] CreateTrainerDTO trainer)
    {
        return await trainerService.CreateTrainer(trainer);
    }

    [HttpDelete("{id}")]
    public async Task<int> Delete(Guid id)
    {
        return await trainerService.DeleteTrainerById(id);
    }
}


