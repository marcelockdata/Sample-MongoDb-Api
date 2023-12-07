using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.Entities;
using Sample.MongoDb.Api.Domain.ValueObjects;

namespace Sample.MongoDb.Api.Infra.Models;

public class RestaurantModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;
    public required string Name { get; set; }
    public EKitchen Kitchen { get; set; }
    public required AddressModel Address { get; set; }
}

public static class RestauranteModelExtension
{
    public static Restaurant MapToDomain(this RestaurantModel document)
    {
        var restaurant = new Restaurant(document.Id, document.Name, document.Kitchen);
        var address = new Address(
            document.Address.Street,
            document.Address.Number,
            document.Address.City,
            document.Address.State,
            document.Address.ZipCode);
        restaurant.AddAddress(address); // atribuir endereço

        return restaurant;
    }
}
