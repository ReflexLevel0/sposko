using Microsoft.AspNetCore.Mvc;
namespace sposko;

[Route("api/[controller]")]
[ApiController]
public class SportTrainingController(ISportTrainingService trainingService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<SportTrainingDTO>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<SportTrainingDTO>>> Get([FromQuery] int? groupId)
    {
        var trainings = new List<SportTrainingDTO>();
        await foreach (var training in trainingService.GetSportTrainings(groupId))
        {
            trainings.Add(training);
        }
        return Ok(trainings);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(SportTrainingDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SportTrainingDTO?>> Get(int id)
    {
        var training = await trainingService.GetSportTrainingById(id);
        return training == null ? NotFound() : Ok(training);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SportTrainingDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<SportTrainingDTO?>> Post([FromBody] CreateSportTrainingDTO training)
    {
        var insertedTraining = await trainingService.CreateSportTraining(training);
        return insertedTraining == null ? Conflict() : Ok(insertedTraining);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(SportGroupDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SportGroupDTO), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SportTrainingDTO?>> Put(int id, [FromBody] UpdateSportTrainingDTO training)
    {
        var updatedTraining = await trainingService.UpdateSportTraining(id, training);
        return updatedTraining == null ? NotFound() : Ok(updatedTraining);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        if (await trainingService.DeleteSportTrainingById(id) == 0) return NotFound();
        return Ok();
    }
}

