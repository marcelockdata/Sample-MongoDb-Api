using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Delete;

public class DeleteRestaurantInput : IRequest<Unit>
{
    public DeleteRestaurantInput(string id)
        => Id = id;

    public string Id { get; set; }
}
