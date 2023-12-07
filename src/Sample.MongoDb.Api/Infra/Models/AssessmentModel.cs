using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Sample.MongoDb.Api.Domain.ValueObjects;

namespace Sample.MongoDb.Api.Infra.Models;

public class AssessmentModel
{
    
    public ObjectId Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public required string RestaurantId { get; set; }
    
    public int Stars { get; set; }
    
    public required string Comment { get;  set; }
}

public static class AssessmentModelExtension
{
    public static Assessment MapToDomain(this AssessmentModel document)
    {
        return new Assessment(document.Stars, document.Comment);
    }
}
