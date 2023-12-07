using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Create;

public interface ICreateRestaurant : IRequestHandler<CreateRestaurantInput, CreateRestaurantOutput> { }
