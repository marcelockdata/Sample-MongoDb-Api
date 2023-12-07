using Sample.MongoDb.Api.Domain.Interfaces;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3;

public class GetRestaurantTop3 : IGetRestaurantTop3
{
    private readonly IRestaurantRepository _repository;

    public GetRestaurantTop3(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<GetRestaurantTop3Output>> Handle(GetRestaurantTop3Input input, CancellationToken cancellationToken)
    {
        var top3 = await _repository.GetTop3();

        var list = top3.Select(_ => new GetRestaurantTop3Output
        {
            Id = _.Key.Id,
            Name = _.Key.Name,
            Kitchen = (int)_.Key.Kitchen,
            City = _.Key.Address.City,
            Stars = _.Value
        });

        return list.ToList();
    }
}