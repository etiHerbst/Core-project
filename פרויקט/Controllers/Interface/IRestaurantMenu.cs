using core.Models;

namespace core.Interfaces;

public interface IRestaurantMenu
{
    // שליפת כל המנות בתפריט
    IEnumerable<Restaurant?> GetAll();

    // שליפת מנה לפי מזהה — null אם לא נמצאה
    Restaurant? GetById(int id);

    // הוספת מנה חדשה לתפריט
    Restaurant Add(Restaurant restaurant);

    void Delete(int id);

    bool Update(int id, Restaurant restaurant);


}
