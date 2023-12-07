using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantName;

public class GetRestaurantNameInput : IRequest<IReadOnlyList<GetRestaurantNameOutput>>
{
    public GetRestaurantNameInput(string name) 
        => Name = name;

    public string Name { get; set; }
}