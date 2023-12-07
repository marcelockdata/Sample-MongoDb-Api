using Sample.MongoDb.Api.Domain.Interfaces;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetAll;

public class GetRestaurantAll : IGetRestaurantAll
{
    private readonly IRestaurantRepository _repository;

    public GetRestaurantAll(IRestaurantRepository repository) 
        => _repository = repository;

    public async Task<IReadOnlyList<GetRestaurantAllOutput>> Handle(GetRestaurantAllInput request, CancellationToken cancellationToken)
    {
        var restaurants = await _repository.GetAll();

        if (restaurants == null)
            return default!;

        var list = restaurants.Select(_ => new GetRestaurantAllOutput
        {
            Id = _.Id,
            Name = _.Name,
            Kitchen = (int)_.Kitchen,
            City = _.Address.City
        });        

        return list.ToList();
    }
}
