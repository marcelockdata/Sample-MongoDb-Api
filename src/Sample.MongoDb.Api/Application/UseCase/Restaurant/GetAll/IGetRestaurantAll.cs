using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetAll;

public interface IGetRestaurantAll : IRequestHandler<GetRestaurantAllInput, IReadOnlyList<GetRestaurantAllOutput>> { }