using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Delete;

public interface IDeleteRestaurant
    : IRequestHandler<DeleteRestaurantInput, Unit>
{ }
