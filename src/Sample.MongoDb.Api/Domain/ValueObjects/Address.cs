using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Sample.MongoDb.Api.Domain.ValueObjects;

public class Address : AbstractValidator<Address>
{
    public Address(
        string street, 
        string number, 
        string city, 
        string state, 
        string zipCode)
    {
        Street = street;
        Number = number;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string Street { get; private set; }

    public string Number { get; private set; }

    public string City { get; private set; }

    public string State { get; private set; }

    public string ZipCode { get; private set; }

    public FluentValidation.Results.ValidationResult ValidationResult { get; set; }

    public bool Validate()
    {
        ValidateStreet();
        ValidateCity();
        ValidadeState();
        ValidateZipCode();

        ValidationResult = Validate(this);

        return ValidationResult.IsValid;
    }

    private void ValidateStreet()
    {
        RuleFor(c => c.Street)
            .NotEmpty().WithMessage("Street cannot be empty.")
            .MaximumLength(50).WithMessage("Street can have a maximum of 50 characters.");
    }

    private void ValidateCity()
    {
        RuleFor(c => c.City)
            .NotEmpty().WithMessage("City cannot be empty.")
            .MaximumLength(100).WithMessage("Street can have a maximum of 100 characters.");
    }

    private void ValidadeState()
    {
        RuleFor(c => c.State)
            .NotEmpty().WithMessage("State cannot be empty.")
            .Length(2).WithMessage("State must have 2 characters.");
    }

    private void ValidateZipCode()
    {
        RuleFor(c => c.ZipCode)
            .NotEmpty().WithMessage("ZipCode cannot be empty.")
            .Length(8).WithMessage("ZipCode must have 8 characters.");
    }
}