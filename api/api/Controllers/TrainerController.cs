using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class TrainerController(ITrainerService trainerService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<TrainerDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TrainerDTO>>> Get()
    {
        var trainers = new List<TrainerDTO>();
        await foreach (var trainer in trainerService.GetTrainers())
        {
            trainers.Add(trainer);
        }
        return Ok(trainers);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TrainerDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrainerDTO?>> Get(Guid id)
    {
        var trainer = await trainerService.GetTrainerById(id);
        return trainer == null ? NotFound() : Ok(trainer);
    }

    [HttpPost]
    [ProducesResponseType(typeof(TrainerDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TrainerDTO?>> Post([FromBody] CreateTrainerDTO trainer)
    {
        var insertedTrainer = await trainerService.CreateTrainer(trainer);
        return insertedTrainer == null ? Conflict() : Created("/api/trainer", insertedTrainer);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(Guid id)
    {
        int changedRows = await trainerService.DeleteTrainerById(id);
        return changedRows == 0 ? NotFound() : Ok();
    }
}


