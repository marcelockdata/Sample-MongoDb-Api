using Sample.MongoDb.Api.Domain.Interfaces;
using Sample.MongoDb.Api.Domain.ValueObjects;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.AssessmentRestaurant;

public class AssessmentRestaurant : IAssessmentRestaurant
{
    private readonly IRestaurantRepository _repository;

    public AssessmentRestaurant(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<AssessmentRestaurantOutput> Handle(AssessmentRestaurantInput input, CancellationToken cancellationToken)
    {
        var restaurant = _repository.GetById(input.Id);

        if (restaurant == null)
            return await Task.FromResult(new AssessmentRestaurantOutput(false, "Error"));

        var assessment = new Assessment(input.Stars, input.Comment);

        _repository.Assessment(input.Id, assessment);

        return await Task.FromResult(new AssessmentRestaurantOutput(true, "Sucess"));
    }
}