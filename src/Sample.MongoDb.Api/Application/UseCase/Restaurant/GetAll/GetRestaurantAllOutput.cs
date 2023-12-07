namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetAll;

public class GetRestaurantAllOutput
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public int Kitchen { get; set; }

    public required string City { get; set; }
}
