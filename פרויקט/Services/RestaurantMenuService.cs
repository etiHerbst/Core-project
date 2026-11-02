using core.Interfaces;
using core.Models;

namespace core.Services;

public class RestaurantMenuService : IRestaurantMenu
{
    private static readonly string[] menu = new[]
    {
        "Antrikot", "sushi", "Amburger", "tunge", "chicken", "shnisel", "meat", "pastarma", "wings", "hot dog"
    };

    // רשימה אחת לכל אורך חיי האפליקציה (השירות רשום כ-Singleton ב-Program.cs)
    private static readonly List<Restaurant> arr = Enumerable.Range(1, 10).Select(index => new Restaurant
    {
        ID = (index + 100),
        Price = Random.Shared.Next(120, 450),
        Dish = menu[Random.Shared.Next(menu.Length)]
    })
    .ToList();

    // קונסטרוקטור ריק — ASP.NET Core יוכל ליצור את השירות באמצעות DI
    public RestaurantMenuService()
    {
    }

    public IEnumerable<Restaurant?> GetAll()
    {
        return arr;
    }

    public Restaurant? GetById(int id)
    {
        return arr.FirstOrDefault(r => r.ID == id);
    }

    public Restaurant Add(Restaurant restaurant)
    {
        // מקצים מזהה חדש שאינו בשימוש
        restaurant.ID = arr.Count == 0 ? 1 : arr.Max(r => r.ID) + 1;
        arr.Add(restaurant);
        return restaurant;
    }

    public void Delete (int id){
        var restaurant= GetById(id);
        if (restaurant is null)
             return;

             arr.Remove(restaurant);

    }

    // public void Update (int id, restaurant restaurant)
    // {
    //     var index= arr.FindIndex(r => id == r.Id && r.Id == restaurant.Id);
    //     if(index==-1)
    //     return;

    //     arr[index]= restaurant;
    // }

    public bool Update(int id, Restaurant updated)
{
    var existing = arr.FirstOrDefault(r => r.ID == id);
    if (existing is null) return false;

    existing.Price = updated.Price;
    existing.Dish = updated.Dish;
    return true;
}


}
