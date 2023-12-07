using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3WithCombo;

public interface IGetRestaurantTop3WithCombo : IRequestHandler<GetRestaurantTop3WithComboInput, IReadOnlyList<GetRestaurantTop3WithComboOutput>> { }