
using Sample.MongoDb.Api.Domain.Interfaces;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Get;

public class GetRestaurant : IGetRestaurant
{
    private readonly IRestaurantRepository _repository;

    public GetRestaurant(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<GetRestaurantOutput> Handle(GetRestaurantInput input, CancellationToken cancellationToken)
    {
        var output = await Task.FromResult(_repository.GetById(input.Id));

        if (output == null)
            return default!;

        return GetRestaurantOutput.FromRestaurant(output);
    }
}
