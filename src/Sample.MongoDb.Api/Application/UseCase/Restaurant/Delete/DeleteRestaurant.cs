using MediatR;
using Sample.MongoDb.Api.Domain.Interfaces;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.Delete;

public class DeleteRestaurant : IDeleteRestaurant
{
    private readonly IRestaurantRepository _repository;

    public DeleteRestaurant(IRestaurantRepository repository)
        => _repository = repository;


    public async Task<Unit> Handle(DeleteRestaurantInput input, CancellationToken cancellationToken)
    {
        var product = await Task.FromResult(_repository.GetById(input.Id));
        ////NotFoundException.ThrowIfNull(product, $"Product '{input.Id}' not found.");
        _repository.Delete(input.Id);
        return Unit.Value;
    }
}
