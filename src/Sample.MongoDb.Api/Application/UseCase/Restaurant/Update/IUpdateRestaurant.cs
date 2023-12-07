using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Update;

public interface IUpdateRestaurant : IRequestHandler<UpdateRestaurantInput, UpdateRestaurantOutput> { }