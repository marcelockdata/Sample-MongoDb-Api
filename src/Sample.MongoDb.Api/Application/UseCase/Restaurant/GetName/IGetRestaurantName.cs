using MediatR;
using System.Collections.Generic;

namespace Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantName;

public interface IGetRestaurantName : IRequestHandler<GetRestaurantNameInput, IReadOnlyList<GetRestaurantNameOutput>> { }
