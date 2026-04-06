using LearningApp.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LearningApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly DataService _data;

    public CategoriesController(DataService data) => _data = data;

    [HttpGet]
    public IActionResult GetAll() => Ok(_data.GetCategories());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var cat = _data.GetCategory(id);
        return cat is null ? NotFound() : Ok(cat);
    }
}
