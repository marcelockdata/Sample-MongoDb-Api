using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Sample.MongoDb.Api.Infra.Models;

public class RestaurantAssessmentModel
{
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonId]
    public required string Id { get; set; }
    public double AvaregeStars { get; set; }
    public required List<RestaurantModel> Restaurant { get; set; }
    public required List<AssessmentModel> Assessments { get; set; }
}
