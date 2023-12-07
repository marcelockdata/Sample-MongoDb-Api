using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Update;

public class UpdateRestaurantInput : IRequest<UpdateRestaurantOutput>
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public int Kitchen { get; set; }

    public required string Street { get; set; }

    public required string Number { get; set; }

    public required string City { get; set; }

    public required string State { get; set; }

    public required string ZipCode { get; set; }
}
