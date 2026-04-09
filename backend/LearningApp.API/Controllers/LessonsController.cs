using LearningApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.API.Controllers;

[ApiController]
[Route("api")]
public class LessonsController : ControllerBase
{
    private readonly DataService _data;

    public LessonsController(DataService data) => _data = data;

    [HttpGet("lessons/{id}")]
    public IActionResult GetLesson(int id)
    {
        var lesson = _data.GetLesson(id);
        return lesson is null ? NotFound() : Ok(lesson);
    }

    [HttpGet("subcategories/{subcategoryId}/lessons")]
    public IActionResult GetBySubcategory(int subcategoryId) =>
        Ok(_data.GetLessonsBySubcategory(subcategoryId));
}
