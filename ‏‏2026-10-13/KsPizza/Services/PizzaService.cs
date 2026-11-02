using KsPizza.Models;
using KsPizza.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace KsPizza.Services
{
    public class PizzaService : IPizzaService
    {
        List<Pizza> Pizzas { get; }
        int nextId = 3;
        public PizzaService()
        {
            Pizzas = new List<Pizza>
            {
                new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
                new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
            };
        }

        public List<Pizza> GetAll() => Pizzas;

        public Pizza Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        public void Add(Pizza pizza)
        {
            pizza.Id = nextId++;
            Pizzas.Add(pizza);
        }

        public void Delete(int id)
        {
            var pizza = Get(id);
            if(pizza is null)
                return;

            Pizzas.Remove(pizza);
        }

        public void Update(int id, Pizza pizza)
        {
            var index = Pizzas.FindIndex(p => id == p.Id && p.Id == pizza.Id);
            if(index == -1)
                return;

            Pizzas[index] = pizza;
        }

        public int Count => Pizzas.Count();
    }
}