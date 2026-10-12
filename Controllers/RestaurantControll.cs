using Microsoft.AspNetCore.Mvc;
using core.Models;

namespace core.Controllers;

[ApiController]
[Route("[controller]")]
public class RestaurantController : ControllerBase
{
     private static readonly string[] menu = new[]
    {
        "Antrikot", "sushi", "Amburger", "tunge", "chicken", "shnisel", "meat", "pastarma", "wings", "hot dog"
    };
    // private readonly ILogger<RestaurantController> _logger;

    //  public RestaurantController(ILogger<RestaurantController> logger)
    // {
    //     _logger = logger;
    // }
    private static List<Restaurant>? arr;

    public RestaurantController()
    {
        arr= Enumerable.Range(1, 10).Select(index => new Restaurant
        {
            ID = (index+100),
            Price = Random.Shared.Next(120, 450),
            Dish = menu[Random.Shared.Next(menu.Length)]
        })
        .ToList();
    }

    
     [HttpGet(Name = "GetRestaurant")]
    public IEnumerable<Restaurant?> Get()
    {
        return arr;
    }
       [HttpGet("{id}")]
    public ActionResult<Restaurant> Get(int ID)
    {
        //TODO basic checks
        var res = arr.FirstOrDefault(N => N.ID == ID); 

        if (res == null)
            return NotFound();
        
        return res;
    }
}
