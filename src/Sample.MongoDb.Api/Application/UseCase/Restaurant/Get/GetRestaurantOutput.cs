using Sample.MongoDb.Api.Domain.Enums;
using DomainEntity = Sample.MongoDb.Api.Domain.Entities;


namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Get;

public class GetRestaurantOutput(
    string id,
    string name,
    EKitchen kitchen)
{
    public string Id { get; private set; } = id;

    public string Name { get; private set; } = name;

    public EKitchen Kitchen { get; private set; } = kitchen;

    public GetAddressOutput? Address { get; private set; }

    public void FromAddress(GetAddressOutput address)
       => Address = address;

    public static GetRestaurantOutput FromRestaurant(DomainEntity.Restaurant document)
    {
        var restaurant = new GetRestaurantOutput(document.Id, document.Name, document.Kitchen);
        var address = new GetAddressOutput(
            document.Address.Street,
            document.Address.Number,
            document.Address.City,
            document.Address.State,
            document.Address.ZipCode);
        restaurant.FromAddress(address);

        return restaurant;
    }
}

public class GetAddressOutput(
    string street,
    string number,
    string city,
    string state,
    string zipCode)
{
    public string Street { get; private set; } = street;

    public string Number { get; private set; } = number;

    public string City { get; private set; } = city;

    public string State { get; private set; } = state;

    public string ZipCode { get; private set; } = zipCode;

}
