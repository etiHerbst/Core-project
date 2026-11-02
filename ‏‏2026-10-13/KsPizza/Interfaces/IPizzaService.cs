using KsPizza.Models;
using System.Collections.Generic;
using System.Linq;

namespace KsPizza.Interfaces
{
    public interface IPizzaService
    {
        List<Pizza> GetAll();

        Pizza Get(int id);

        void Add(Pizza pizza);

        void Delete(int id);

        void Update(int id, Pizza pizza);

        int Count { get; }
    }
}