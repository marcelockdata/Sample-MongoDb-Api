using Sample.MongoDb.Api.Domain.Interfaces;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3WithCombo;

public class GetRestaurantTop3WithCombo : IGetRestaurantTop3WithCombo
{
    private readonly IRestaurantRepository _repository;

    public GetRestaurantTop3WithCombo(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<GetRestaurantTop3WithComboOutput>> Handle(GetRestaurantTop3WithComboInput input, CancellationToken cancellationToken)
    {
        var top3_WithLookup = await _repository.GetTop3_WithLookup();

        var list = top3_WithLookup.Select(_ => new GetRestaurantTop3WithComboOutput
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
