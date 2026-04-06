using LearningApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.API.Controllers;

[ApiController]
[Route("api/lessons/{lessonId}")]
public class QuizController : ControllerBase
{
    private readonly DataService _data;

    public QuizController(DataService data) => _data = data;

    [HttpGet("quiz")]
    public IActionResult GetQuiz(int lessonId)
    {
        var quiz = _data.GetQuizByLesson(lessonId);
        return quiz is null ? NotFound() : Ok(quiz);
    }
}
