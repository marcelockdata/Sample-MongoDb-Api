using FluentValidation;
using FluentValidation.Results;
using Sample.MongoDb.Api.Domain.Enums;
using Sample.MongoDb.Api.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Sample.MongoDb.Api.Domain.Entities;

public class Restaurant : AbstractValidator<Restaurant>
{
    public Restaurant() { }

    public Restaurant(
        string name,
        EKitchen kitchen)
    {
        Name = name;
        Kitchen = kitchen;
        Assessment = [];
    }

    public Restaurant(
        string id, 
        string name, 
        EKitchen kitchen)
    {
        Id = id;
        Name = name;
        Kitchen = kitchen;
        Assessment = [];
    }

    public string Id { get; private set; }

    public string Name { get; private set; }

    public EKitchen Kitchen { get; private set; }

    public Address Address { get; private set; }

    public List<Assessment> Assessment { get; private set; }

    public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

    public void AddAddress(Address address)
        => Address = address;

    public void AddAssessment(Assessment assessment)
        => Assessment.Add(assessment);

    public virtual bool Validate()
    {
        ValidateName();
        ValidationResult = Validate(this);

        ValidateAddress();

        return ValidationResult.IsValid;
    }

    private void ValidateName()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name cannot be empty.")
            .MaximumLength(30).WithMessage("Name can have a maximum of 30 characters.");
    }

    private void ValidateAddress()
    {
        if (Address.Validate())
            return;

        foreach (var erro in Address.ValidationResult.Errors)
            ValidationResult.Errors.Add(erro);
    }
}
