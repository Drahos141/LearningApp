using LearningApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.API.Controllers;

[ApiController]
[Route("api/lessons/{lessonId}")]
public class MiniGameController : ControllerBase
{
    private readonly DataService _data;

    public MiniGameController(DataService data) => _data = data;

    [HttpGet("minigame")]
    public IActionResult GetMiniGame(int lessonId)
    {
        var game = _data.GetMiniGameByLesson(lessonId);
        return game is null ? NotFound() : Ok(game);
    }
}
