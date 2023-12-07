using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Get;

public class GetRestaurantInput : IRequest<GetRestaurantOutput>
{
    public GetRestaurantInput(string id)
      => Id = id;

    public string Id { get; set; }
}
