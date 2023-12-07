using Sample.MongoDb.Api.Domain.Entities;
using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.ValueObjects;

namespace Sample.MongoDb.Api.Domain.Interfaces;

public interface IRestaurantRepository
{
    void Insert(Restaurant restaurant);

    Task<IReadOnlyList<Restaurant>> GetAll();

    Restaurant? GetById(string id);

    bool Update(Restaurant restaurant);

    bool UpdateKitchen(string id, EKitchen kitchen);

    IEnumerable<Restaurant> GetByName(string name);

    void Assessment(string restaurantId, Assessment assessment);

    Task<Dictionary<Restaurant, double>> GetTop3();

    Task<Dictionary<Restaurant, double>> GetTop3_WithLookup();

    (long, long) Delete(string restaurantId);
}
