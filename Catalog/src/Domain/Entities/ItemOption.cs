﻿namespace YourBrand.Catalog.Domain.Entities;

public class ItemOption : Entity<int>
{
    public string ItemId { get; set; } = null!;

    public Item Item { get; set; } = null!;

    public string OptionId { get; set; } = null!;

    public Option Option { get; set; } = null!;

    public bool? IsSelected { get; set; }

    // Add fields for default values
}