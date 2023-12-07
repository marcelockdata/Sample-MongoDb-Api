namespace Sample.MongoDb.Api.Infra.Models;

public class AddressModel
{
    public required string Street { get; set; }
    public required string Number { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
}
