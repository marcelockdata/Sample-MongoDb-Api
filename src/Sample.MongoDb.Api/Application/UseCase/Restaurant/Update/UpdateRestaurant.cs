using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.Interfaces;
using DomainEntity = Sample.MongoDb.Api.Domain.Entities;
using ValueObjects = Sample.MongoDb.Api.Domain.ValueObjects;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Update;

public class UpdateRestaurant : IUpdateRestaurant
{
    private readonly IRestaurantRepository _repository;

    public UpdateRestaurant(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<UpdateRestaurantOutput> Handle(UpdateRestaurantInput input, CancellationToken cancellationToken)
    {
        var restaurant = _repository.GetById(input.Id);

        if (restaurant == null)
            return await Task.FromResult(new UpdateRestaurantOutput(false, "Error"));

        var kitchen = EKitchenHelper.FromKitchen(input.Kitchen);

        restaurant = new DomainEntity.Restaurant(input.Id, input.Name, kitchen);
        var address = new ValueObjects.Address(
            input.Street,
            input.Number,
            input.City,
            input.State,
            input.ZipCode);

        restaurant.AddAddress(address);
       
        if (!restaurant.Validate())
        {
            return await Task.FromResult(new UpdateRestaurantOutput(false, "Error"));
        }

        _repository.Update(restaurant);

        return await Task.FromResult(new UpdateRestaurantOutput(true, "Sucess"));

    }
}
