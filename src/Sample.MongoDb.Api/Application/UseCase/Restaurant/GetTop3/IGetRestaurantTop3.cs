using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3;

public interface IGetRestaurantTop3 : IRequestHandler<GetRestaurantTop3Input, IReadOnlyList<GetRestaurantTop3Output>> { }