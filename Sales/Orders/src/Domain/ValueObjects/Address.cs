using YourBrand.Orders.Domain;
using YourBrand.Orders.Domain.Entities;
using YourBrand.Orders.Domain.Events;

namespace YourBrand.Orders.Domain.ValueObjects;

public class Address
{
    // Street
    public string Thoroughfare { get; set; } = null!;

    // Street number
    public string? Premises { get; set; }

    // Suite
    public string? SubPremises { get; set; }

    public string PostalCode { get; set; } = null!;

    // Town or City
    public string Locality { get; set; } = null!;

    // County
    public string SubAdministrativeArea { get; set; } = null!;

    // State
    public string AdministrativeArea { get; set; } = null!;

    public string Country { get; set; } = null!;
}
