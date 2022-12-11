﻿namespace YourBrand.Catalog.Domain.Entities;

public class Attribute : Entity<string>
{
    protected Attribute() { }

    public Attribute(string name)
        : base(Guid.NewGuid().ToString())
    {
        Name = name;
    }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public AttributeGroup? Group { get; set; }

    public ProductGroup? ProductGroup { get; set; }

    public bool ForVariant { get; set; }

    public bool IsMainAttribute { get; set; }

    public List<Product> Products { get; } = new List<Product>();

    public List<AttributeValue> Values { get; } = new List<AttributeValue>();
}
