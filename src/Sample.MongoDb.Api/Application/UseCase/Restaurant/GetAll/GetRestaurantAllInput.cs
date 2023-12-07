using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetAll;

public class GetRestaurantAllInput : IRequest<IReadOnlyList<GetRestaurantAllOutput>> { }