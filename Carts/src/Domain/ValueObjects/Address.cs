using YourBrand.Carts.Domain;
using YourBrand.Carts.Domain.Entities;
using YourBrand.Carts.Domain.Events;

namespace YourBrand.Carts.Domain.ValueObjects;

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
