using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.UpdateRestaurantKitchen;

public class UpdateRestaurantKitchenInput : IRequest<UpdateRestaurantKitchenOutput>
{
    public required string Id { get; set; }

    public int Kitchen { get; set; }
}
