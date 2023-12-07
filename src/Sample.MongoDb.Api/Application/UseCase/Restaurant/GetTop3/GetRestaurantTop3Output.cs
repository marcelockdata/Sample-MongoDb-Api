namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3;

public class GetRestaurantTop3Output
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public int Kitchen { get; set; }

    public required string City { get; set; }

    public double Stars { get; set; }
}
