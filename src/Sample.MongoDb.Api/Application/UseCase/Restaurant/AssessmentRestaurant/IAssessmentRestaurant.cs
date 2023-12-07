using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.AssessmentRestaurant;

public interface IAssessmentRestaurant : IRequestHandler<AssessmentRestaurantInput, AssessmentRestaurantOutput> { }