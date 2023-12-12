using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Sample.MongoDb.Api.Domain.Entities;
using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.Interfaces;
using Sample.MongoDb.Api.Domain.ValueObjects;
using Sample.MongoDb.Api.Infra.Models;

namespace Sample.MongoDb.Api.Infra.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    IMongoCollection<RestaurantModel> _restaurant;
    IMongoCollection<AssessmentModel> _assessment;


    public RestaurantRepository(MongoDB mongoDB)
    {
        _restaurant = mongoDB.DB.GetCollection<RestaurantModel>("restaurants");
        _assessment = mongoDB.DB.GetCollection<AssessmentModel>("assessments");
    }

    public void Insert(Restaurant restaurant)
    {
        var document = new RestaurantModel
        {
            Name = restaurant.Name,
            Kitchen = restaurant.Kitchen,
            Address = new AddressModel
            {
                Street = restaurant.Address.Street,
                Number = restaurant.Address.Number,
                City = restaurant.Address.City,
                ZipCode = restaurant.Address.ZipCode,
                State = restaurant.Address.State
            }
        };

        _restaurant.InsertOne(document);
    }

    public async Task<IReadOnlyList<Restaurant>> GetAll()
    {
        var restaurants = new List<Restaurant>();

        await _restaurant.AsQueryable().ForEachAsync(d =>
        {
            var r = new Restaurant(d.Id.ToString(), d.Name, d.Kitchen);
            var a = new Address(d.Address.Street, d.Address.Number, d.Address.City, d.Address.State, d.Address.ZipCode);
            r.AddAddress(a);
            restaurants.Add(r);
        });

        return restaurants;
    }

    public Restaurant GetById(string id)
    {
        var document = _restaurant.AsQueryable().FirstOrDefault(_ => _.Id == id);

        //if (document == null)
        //    return null;

        return document!.MapToDomain();
    }

    public bool Update(Restaurant restaurant)
    {
        var document = new RestaurantModel
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Kitchen = restaurant.Kitchen,
            Address = new AddressModel
            {
                Street = restaurant.Address.Street,
                Number = restaurant.Address.Number,
                City = restaurant.Address.City,
                ZipCode = restaurant.Address.ZipCode,
                State = restaurant.Address.State
            }
        };

        var result = _restaurant.ReplaceOne(_ => _.Id == document.Id, document);

        return result.ModifiedCount > 0;
    }


    public IEnumerable<Restaurant> GetByName(string name)
    {
        var restaurants = new List<Restaurant>();

        _restaurant.AsQueryable()
            .Where(_ => _.Name.ToLower().Contains(name.ToLower()))
            .ToList()
            .ForEach(d => restaurants.Add(d.MapToDomain()));

        return restaurants;
    }

    public bool UpdateKitchen(string id, EKitchen kitchen)
    {
        var atualizacao = Builders<RestaurantModel>.Update.Set(_ => _.Kitchen, kitchen);

        var resultado = _restaurant.UpdateOne(_ => _.Id == id, atualizacao);

        return resultado.ModifiedCount > 0;
    }

    public void Assessment(string restaurantId, Assessment assessment)
    {
        var document = new AssessmentModel
        {
            RestaurantId = restaurantId,
            Stars = assessment.Stars,
            Comment = assessment.Comment
        };

        _assessment.InsertOne(document);
    }

    public async Task<Dictionary<Restaurant, double>> GetTop3()
    {
        var result = new Dictionary<Restaurant, double>();

        var top3 = _assessment.Aggregate()
            .Group(_ => _.RestaurantId, g => new { RestaurantId = g.Key, AverageStars = g.Average(a => a.Stars) })
            .SortByDescending(_ => _.AverageStars)
            .Limit(3);

        await top3.ForEachAsync(_ =>
        {
            var restaurant = GetById(_.RestaurantId);

            _assessment.AsQueryable()
                .Where(a => a.RestaurantId == _.RestaurantId)
                .ToList()
                .ForEach(a => restaurant!.AddAssessment(a.MapToDomain()));

            result.Add(restaurant!, _.AverageStars);
        });

        return result;
    }

    public async Task<Dictionary<Restaurant, double>> GetTop3_WithLookup()
    {
        var result = new Dictionary<Restaurant, double>();

        var top3 = _assessment.Aggregate()
            .Group(_ => _.RestaurantId, g => new { RestaurantId = g.Key, AverageStars = g.Average(a => a.Stars) })
            .SortByDescending(_ => _.AverageStars)
            .Limit(3)
            .Lookup<RestaurantModel, RestaurantAssessmentModel>("restaurants", "RestaurantId", "Id", "Restaurant")
            .Lookup<AssessmentModel, RestaurantAssessmentModel>("assessments", "Id", "RestaurantId", "Assessments");

        await top3.ForEachAsync(_ =>
        {
            if (!_.Restaurant.Any())
                return;

            var restaurant = new Restaurant(_.Id, _.Restaurant[0].Name, _.Restaurant[0].Kitchen);
            var address = new Address(
                _.Restaurant[0].Address.Street,
                _.Restaurant[0].Address.Number,
                _.Restaurant[0].Address.City,
                _.Restaurant[0].Address.State,
                _.Restaurant[0].Address.ZipCode);

            restaurant.AddAddress(address);

            _.Assessments.ForEach(a => restaurant.AddAssessment(a.MapToDomain()));

            result.Add(restaurant, _.AvaregeStars);
        });

        return result;
    }

    public (long, long) Delete(string restaurantId)
    {
        var resultadoAvaliacoes = _assessment.DeleteMany(_ => _.RestaurantId == restaurantId);
        var resultadoRestaurante = _restaurant.DeleteOne(_ => _.Id == restaurantId);

        return (resultadoRestaurante.DeletedCount, resultadoAvaliacoes.DeletedCount);
    }

    public async Task<IEnumerable<Restaurant>> ObterPorBuscaTextual(string text)
    {
        var restaurants = new List<Restaurant>();

        var filter = Builders<RestaurantModel>.Filter.Text(text);

        await _restaurant
            .AsQueryable()
            .Where(_ => filter.Inject())
            .ForEachAsync(d => restaurants.Add(d.MapToDomain()));

        return restaurants;
    }

}
