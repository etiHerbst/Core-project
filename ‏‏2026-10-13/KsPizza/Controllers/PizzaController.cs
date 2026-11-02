using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using KsPizza.Models;
using KsPizza.Services;
using KsPizza.Interfaces;


namespace KsPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private IPizzaService pizzaService ;
        public PizzaController(IPizzaService pizzaService)
        {
            this.pizzaService = pizzaService;
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() =>
            pizzaService.GetAll();


        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = pizzaService.Get(id);

            if (pizza == null)
                return NotFound();

            return pizza;
        }

        [HttpPost] 
        public IActionResult Create(Pizza pizza)
        {
            pizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new {id=pizza.Id}, pizza);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();

            var existingPizza = pizzaService.Get(id);
            if (existingPizza is null)
                return  NotFound();

            pizzaService.Update(id, pizza);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pizza = pizzaService.Get(id);
            if (pizza is null)
                return  NotFound();

            pizzaService.Delete(id);

            return Content(pizzaService.Count.ToString());
        }
    }
}