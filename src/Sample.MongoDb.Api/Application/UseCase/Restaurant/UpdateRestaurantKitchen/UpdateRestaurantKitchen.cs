using Sample.MongoDb.Api.Application.UseCase.Restaurant.Update;
using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.Interfaces;
using Sample.MongoDb.Api.Infra.Repositories;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.UpdateRestaurantKitchen;

public class UpdateRestaurantKitchen : IUpdateRestaurantKitchen
{
    private readonly IRestaurantRepository _repository;

    public UpdateRestaurantKitchen(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<UpdateRestaurantKitchenOutput> Handle(UpdateRestaurantKitchenInput input, CancellationToken cancellationToken)
    {
        var restaurant = _repository.GetById(input.Id);

        if (restaurant == null)
            return await Task.FromResult(new UpdateRestaurantKitchenOutput(false, "Error"));

        var kitchen = EKitchenHelper.FromKitchen(input.Kitchen);

        _repository.UpdateKitchen(input.Id, kitchen);

        return await Task.FromResult(new UpdateRestaurantKitchenOutput(true, "Sucess"));


    }
}
