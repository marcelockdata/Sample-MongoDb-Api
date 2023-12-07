using MediatR;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3WithCombo;

public class GetRestaurantTop3WithComboInput : IRequest<IReadOnlyList<GetRestaurantTop3WithComboOutput>> { }