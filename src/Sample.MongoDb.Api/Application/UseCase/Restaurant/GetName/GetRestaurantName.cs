using Sample.MongoDb.Api.Domain.Interfaces;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantName;

public class GetRestaurantName : IGetRestaurantName
{
    private readonly IRestaurantRepository _repository;

    public GetRestaurantName(IRestaurantRepository repository)
        => _repository = repository;

    public async Task<IReadOnlyList<GetRestaurantNameOutput>> Handle(GetRestaurantNameInput input, CancellationToken cancellationToken)
    {

        var restaurantes = await Task.FromResult(_repository.GetByName(input.Name));

        if (restaurantes == null)
            return default!;

        var list = restaurantes.Select(_ => new GetRestaurantNameOutput
        {
            Id = _.Id,
            Name = _.Name,
            Kitchen = (int)_.Kitchen,
            City = _.Address.City
        });

        return list.ToList();
    }
}