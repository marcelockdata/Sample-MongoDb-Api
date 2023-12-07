using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Infra.Models;

namespace Sample.MongoDb.Api.Infra;

public class MongoDB
{
    public IMongoDatabase DB { get; }

    public MongoDB(IConfiguration configuration)
    {
        try
        {
            var client = new MongoClient(configuration["ConnectionString"]);
            DB = client.GetDatabase(configuration["DataBase"]);
            MapToModel();
        }
        catch (Exception ex)
        {
            throw new MongoException("Unable to connect to MongoDB", ex);
        }
    }

    private static void MapToModel()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(RestaurantModel)))
        {
            BsonClassMap.RegisterClassMap<RestaurantModel>(i =>
            {
                i.AutoMap();
                i.MapIdMember(c => c.Id);
                i.MapMember(c => c.Kitchen).SetSerializer(new EnumSerializer<EKitchen>(BsonType.Int32));
                i.SetIgnoreExtraElements(true);
            });
        }
    }
}
