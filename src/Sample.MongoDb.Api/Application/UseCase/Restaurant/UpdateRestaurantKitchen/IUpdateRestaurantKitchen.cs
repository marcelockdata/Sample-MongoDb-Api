using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.UpdateRestaurantKitchen;

public interface IUpdateRestaurantKitchen : IRequestHandler<UpdateRestaurantKitchenInput, UpdateRestaurantKitchenOutput> { }
