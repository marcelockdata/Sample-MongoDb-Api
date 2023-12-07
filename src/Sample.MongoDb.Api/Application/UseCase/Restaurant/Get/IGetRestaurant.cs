using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Get;

public interface IGetRestaurant : IRequestHandler<GetRestaurantInput, GetRestaurantOutput> { }
