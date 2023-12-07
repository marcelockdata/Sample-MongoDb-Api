using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sample.MongoDb.Api.Api.ApiModels;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.AssessmentRestaurant;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.Create;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.Delete;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.Get;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.GetAll;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantName;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.GetRestaurantTop3WithCombo;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.Update;
using Sample.MongoDb.Api.Application.UseCase.Restaurant.UpdateRestaurantKitchen;

namespace Sample.MongoDb.Api.Api.EndPoints;

public static class RestaurantEndPoints
{
    public static void ConfigureProductEndpoints(this WebApplication app)
    {
        app.MapPost("/api/restaurant",
           static async (IMediator mediator,
               [FromBody] CreateRestaurantInput input,
               CancellationToken cancellationToken)
           =>
           {
               var output = await mediator.Send(input, cancellationToken);
               return Results.Created($"/api/restaurant/{output.Status}", new ApiResponse<CreateRestaurantOutput>(output));
           })
            .WithName("CreateRestaurant")
            .Accepts<CreateRestaurantInput>("application/json")
            .Produces<ApiResponse<CreateRestaurantOutput>>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status422UnprocessableEntity);


        app.MapGet("/api/restaurants",
           static async (IMediator mediator,
               CancellationToken cancellationToken)
         =>
           {
               var output = await mediator.Send(new GetRestaurantAllInput(), cancellationToken);
               return Results.Ok(new ApiResponseList<GetRestaurantAllOutput>(output));

           })
            .WithName("GetRestaurantAll")
            .Produces<ApiResponseList<GetRestaurantAllOutput>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

        app.MapGet("/api/restaurant/{id}",
           static async (IMediator mediator,
               [FromRoute] string id,
               CancellationToken cancellationToken)
          =>
           {
               var output = await mediator.Send(new GetRestaurantInput(id), cancellationToken);
               return Results.Ok(new ApiResponse<GetRestaurantOutput>(output));

           })
            .WithName("GetRestaurant")
            .Produces<ApiResponse<GetRestaurantOutput>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);

         app.MapPut("/api/restaurant",
               static async (IMediator mediator,
                   [FromBody] UpdateRestaurantInput input,
                   CancellationToken cancellationToken)
               =>
               {
                   var output = await mediator.Send(input, cancellationToken);
                   return Results.Ok(new ApiResponse<UpdateRestaurantOutput>(output));
               })
                .WithName("UpdateRestaurant")
                .Accepts<CreateRestaurantInput>("application/json")
                .Produces<ApiResponse<CreateRestaurantOutput>>(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status422UnprocessableEntity);

        app.MapPatch("/api/restaurant",
              static async (IMediator mediator,
                  [FromBody] UpdateRestaurantKitchenInput input,
                  CancellationToken cancellationToken)
              =>
              {
                  var output = await mediator.Send(input, cancellationToken);
                  return Results.Ok(new ApiResponse<UpdateRestaurantKitchenOutput>(output));
              })
               .WithName("UpdateRestaurantKitchen")
               .Accepts<UpdateRestaurantKitchenInput>("application/json")
               .Produces<ApiResponse<UpdateRestaurantKitchenOutput>>(StatusCodes.Status200OK)
               .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);


        app.MapGet("/api/restaurantName/{name}",
           static async (IMediator mediator,
               [FromQuery] string name,
               CancellationToken cancellationToken)
          =>
           {
               var output = await mediator.Send(new GetRestaurantNameInput(name), cancellationToken);
               return Results.Ok(new ApiResponseList<GetRestaurantNameOutput>(output));

           })
            .WithName("GetRestaurantName")
            .Produces<ApiResponse<GetRestaurantOutput>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);


        app.MapPatch("/api/assessment",
             static async (IMediator mediator,
                 [FromBody] AssessmentRestaurantInput input,
                 CancellationToken cancellationToken)
             =>
             {
                 var output = await mediator.Send(input, cancellationToken);
                 return Results.Ok(new ApiResponse<AssessmentRestaurantOutput>(output));
             })
              .WithName("Assessment")
              .Accepts<AssessmentRestaurantInput>("application/json")
              .Produces<ApiResponse<AssessmentRestaurantOutput>>(StatusCodes.Status200OK)
              .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);


        app.MapGet("/api/restaurantsTop3",
             static async (IMediator mediator,
             CancellationToken cancellationToken)
            =>
             { 
                var output = await mediator.Send(new GetRestaurantTop3Input(), cancellationToken);
                return Results.Ok(new ApiResponseList<GetRestaurantTop3Output>(output));

             })
              .WithName("RestaurantsTop3")
              .Produces<ApiResponseList<GetRestaurantTop3Output>>(StatusCodes.Status200OK)
              .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

        app.MapGet("/api/restaurantsTop3WithCombo",
             static async (IMediator mediator,
             CancellationToken cancellationToken)
            =>
             {
                 var output = await mediator.Send(new GetRestaurantTop3WithComboInput(), cancellationToken);
                 return Results.Ok(new ApiResponseList<GetRestaurantTop3WithComboOutput>(output));

             })
              .WithName("RestaurantsTop3WithCombo")
              .Produces<ApiResponseList<GetRestaurantTop3WithComboOutput>>(StatusCodes.Status200OK)
              .Produces<ProblemDetails>(StatusCodes.Status400BadRequest);

        app.MapDelete("/api/restaurant/{id}",
           static async (IMediator mediator,
           [FromRoute] string id,
           CancellationToken cancellationToken)
            =>
           {
               var output = await mediator.Send(new DeleteRestaurantInput(id), cancellationToken);
               return Results.NoContent();

           })
            .WithName("DeleteRestaurant")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound);
    }

}
