using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.AssessmentRestaurant;

public class AssessmentRestaurantInput : IRequest<AssessmentRestaurantOutput>
{
    public required string Id { get; set; }

    public int Stars { get; set; }

    public required string Comment { get; set; }
}
