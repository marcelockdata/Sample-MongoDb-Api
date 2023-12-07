using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.Interfaces;
using DomainEntity = Sample.MongoDb.Api.Domain.Entities;
using ValueObjects = Sample.MongoDb.Api.Domain.ValueObjects;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Create;

public class CreateRestaurant : ICreateRestaurant
{
    private readonly IRestaurantRepository _repository;

    public CreateRestaurant(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<CreateRestaurantOutput> Handle(CreateRestaurantInput input, CancellationToken cancellationToken)
    {
        var kitchen = EKitchenHelper.FromKitchen(input.Kitchen);

        var restaurant = new DomainEntity.Restaurant(input.Name, kitchen);
        var endereco = new ValueObjects.Address(
            input.Street,
            input.Number,
            input.City,
            input.State,
            input.ZipCode);

        restaurant.AddAddress(endereco);

        if (!restaurant.Validate())
        {
            return await Task.FromResult(new CreateRestaurantOutput(false, "Error"));
        }

        _repository.Insert(restaurant);

        return await Task.FromResult(new CreateRestaurantOutput(true, "Sucess"));
    }
}