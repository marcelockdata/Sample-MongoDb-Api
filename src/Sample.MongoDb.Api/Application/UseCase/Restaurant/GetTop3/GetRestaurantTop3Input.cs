using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3;

public class GetRestaurantTop3Input: IRequest<IReadOnlyList<GetRestaurantTop3Output>> { }