using Microsoft.AspNetCore.Mvc;
using core.Models;
using core.Interfaces;
namespace core.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly IRestaurantMenu _menu;

    public RestaurantController(IRestaurantMenu menu)  // DI מזריק את השירות
    {
        _menu = menu;
    }

    [HttpGet(Name = "GetRestaurant")]
    public ActionResult<IEnumerable<Restaurant>> GetAll()
    {
        return Ok(_menu.GetAll());
    }

    [HttpGet("{id:int}")]
    public ActionResult<Restaurant> GetById(int id)
    {
        var restaurant = _menu.GetById(id);
        return restaurant is null ? NotFound() : Ok(restaurant);
    }

    [HttpPost]
    public ActionResult<Restaurant> Create([FromBody] Restaurant restaurant)
    {
        var created = _menu.Add(restaurant);
        return CreatedAtAction(nameof(GetById), new { id = created.ID }, created);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (_menu.GetById(id) is null)
            return NotFound();

        _menu.Delete(id);
        return NoContent();
    }
   
   
   
    [HttpPut("{id:int}")]
public IActionResult Update(int id, [FromBody] Restaurant restaurant)
{
    return _menu.Update(id, restaurant) ? NoContent() : NotFound();
}
}