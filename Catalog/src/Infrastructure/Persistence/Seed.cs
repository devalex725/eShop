﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using YourBrand.Catalog.Domain.Entities;
using YourBrand.Catalog.Domain.Enums;

namespace YourBrand.Catalog.Infrastructure.Persistence;

public static class Seed
{
    public static async Task SeedData(ApplicationDbContext context)
    {
        context.ProductGroups.Add(new ProductGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Products",
            Description = null
        });

        context.ProductGroups.Add(new ProductGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Clothes",
            Description = null
        });

        context.ProductGroups.Add(new ProductGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Food",
            Description = null
        });

        await context.SaveChangesAsync();

        await CreateShirt2(context);

        await CreateKebabPlate(context);

        await CreateHerrgardsStek(context);

        await CreateKorg(context);

        await CreatePizza(context);

        await CreateSalad(context);

        await context.SaveChangesAsync();
    }

    public static async Task CreateShirt(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Randing t-shirt",
            Description = "Stilren t-shirt med randigt mönster",
            HasVariants = true,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Clothes")
        };

        context.Products.Add(product);

        var option = new Domain.Entities.Attribute()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Size",
            ForVariant = true
        };

        product.Attributes.Add(option);

        var valueSmall = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Small"
        };

        option.Values.Add(valueSmall);

        var valueMedium = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Medium"
        };

        option.Values.Add(valueMedium);

        var valueLarge = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Large"
        };

        option.Values.Add(valueLarge);

        product.Attributes.Add(option);

        var variantSmall = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Small",
            ItemId = "12345667",
            GTIN = "4345547457457",
        };

        variantSmall.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option,
            Value = valueSmall
        });

        product.Variants.Add(variantSmall);

        var variantMedium = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Medium",
            ItemId = "4465645645",
            GTIN = "543453454567",
        };

        variantMedium.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option,
            Value = valueMedium
        });

        product.Variants.Add(variantMedium);

        var variantLarge = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Large",
            ItemId = "233423544545",
            GTIN = "6876345345345",
        };

        variantLarge.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option,
            Value = valueLarge
        });

        product.Variants.Add(variantLarge);
    }

    public static async Task CreateShirt2(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "T-shirt",
            Description = "T-shirt i olika färger",
            HasVariants = true,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Clothes"),
            Visibility = ProductVisibility.Listed
        };

        context.Products.Add(product);

        var attr = new Domain.Entities.Attribute()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Size",
            ForVariant = true
        };

        product.Attributes.Add(attr);

        var valueSmall = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Small"
        };

        attr.Values.Add(valueSmall);

        var valueMedium = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Medium"
        };

        attr.Values.Add(valueMedium);

        var valueLarge = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Large"
        };

        attr.Values.Add(valueLarge);

        product.Attributes.Add(attr);

        var option2 = new Domain.Entities.Attribute()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Color",
            ForVariant = true,
            IsMainAttribute = true
        };

        product.Attributes.Add(option2);

        var valueBlue = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Blue"
        };

        option2.Values.Add(valueBlue);

        var valueRed = new AttributeValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Red"
        };

        option2.Values.Add(valueRed);

        ///*

        var variantBlueSmall = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Blue S",
            ItemId = "TSHIRT-BLUE-S",
            GTIN = "4345547457457",
            Price = 120,
        };

        variantBlueSmall.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = attr,
            Value = valueSmall
        });

        variantBlueSmall.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option2,
            Value = valueBlue
        });

        product.Variants.Add(variantBlueSmall);

        //*/

        var variantBlueMedium = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Blue M",
            ItemId = "TSHIRT-BLUE-M",
            GTIN = "543453454567",
            Price = 120
        };

        variantBlueMedium.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = attr,
            Value = valueMedium
        });

        variantBlueMedium.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option2,
            Value = valueBlue
        });

        product.Variants.Add(variantBlueMedium);

        var variantBlueLarge = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Blue L",
            ItemId = "TSHIRT-BLUE-L",
            GTIN = "6876345345345",
            Price = 60,
        };

        variantBlueLarge.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = attr,
            Value = valueLarge
        });

        variantBlueLarge.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option2,
            Value = valueBlue
        });

        product.Variants.Add(variantBlueLarge);

        /////

        var variantRedSmall = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Red S",
            ItemId = "TSHIRT-RED-S",
            GTIN = "4345547457457",
            Price = 120,
        };

        variantRedSmall.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = attr,
            Value = valueSmall
        });

        variantRedSmall.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option2,
            Value = valueRed
        });

        product.Variants.Add(variantRedSmall);

        var variantRedMedium = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Red M",
            ItemId = "TSHIRT-RED-M",
            GTIN = "543453454567",
            Price = 120,
        };

        variantRedMedium.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = attr,
            Value = valueMedium
        });

        variantRedMedium.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option2,
            Value = valueRed
        });

        product.Variants.Add(variantRedMedium);

        var variantRedLarge = new ProductVariant()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Red L",
            ItemId = "TSHIRT-RED-L",
            GTIN = "6876345345345",
            Price = 120,
        };

        variantRedLarge.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = attr,
            Value = valueLarge
        });

        variantRedLarge.AttributeValues.Add(new ProductVariantAttributeValue()
        {
            Attribute = option2,
            Value = valueRed
        });

        product.Variants.Add(variantRedLarge);

        var textOption = new Domain.Entities.Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Custom text",
            OptionType = OptionType.TextValue
        };

        product.Options.Add(textOption);
    }

    public static async Task CreateKebabPlate(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Kebabtallrik",
            Description = "Dönnerkebab, nyfriterad pommes frites, sallad, och sås",
            Price = 89,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Food")
        };

        context.Products.Add(product);

        var option = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sås"
        };

        product.Options.Add(option);

        await context.SaveChangesAsync();

        var valueSmall = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Mild"
        };

        option.Values.Add(valueSmall);

        var valueMedium = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stark"
        };

        option.Values.Add(valueMedium);

        var valueLarge = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Blandad"
        };

        option.DefaultValue = valueSmall;

        option.Values.Add(valueLarge);
    }

    public static async Task CreateHerrgardsStek(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Herrgårdsstek",
            Description = "Vår fina stek med pommes och vår hemlagade bearnaise sås",
            Price = 179,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Food")
        };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        var optionDoneness = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Stekning"
        };

        product.Options.Add(optionDoneness);

        await context.SaveChangesAsync();

        optionDoneness.Values.Add(new OptionValue()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Rare",
            Seq = 1
        });

        var optionMediumRare = new OptionValue()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Medium Rare",
            Seq = 2
        };

        optionDoneness.Values.Add(optionMediumRare);

        optionDoneness.Values.Add(new OptionValue()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Well Done",
            Seq = 3
        });

        optionDoneness.DefaultValue = optionMediumRare;

        var optionSize = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Extra stor - 50 g mer",
            Price = 15
        };

        product.Options.Add(optionSize);

        await context.SaveChangesAsync();
    }

    public static async Task CreateKorg(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Korg",
            Description = "En korg med smårätter",
            Price = 179,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Food")
        };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        var ratterGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Rätter",
            Max = 7
        };

        product.OptionGroups.Add(ratterGroup);

        var optionFalafel = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Falafel",
            OptionType = OptionType.NumericalValue,
            Group = ratterGroup
        };

        product.Options.Add(optionFalafel);

        var optionChickenWing = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Spicy Chicken Wing",
            OptionType = OptionType.NumericalValue,
            Group = ratterGroup
        };

        product.Options.Add(optionChickenWing);

        var optionRib = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Rib",
            OptionType = OptionType.NumericalValue,
            Group = ratterGroup
        };

        product.Options.Add(optionRib);

        await context.SaveChangesAsync();


        var extraGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Extra"
        };

        product.OptionGroups.Add(extraGroup);

        await context.SaveChangesAsync();

        var optionSauce = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sås",
            OptionType = OptionType.YesOrNo,
            Price = 10,
            Group = extraGroup
        };

        product.Options.Add(optionSauce);

        /*
        optionSauce.Values.Add(new OptionValue() {
            Name = "Favoritsås", 
        });
        */

        await context.SaveChangesAsync();
    }

    public static async Task CreatePizza(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Pizza",
            Description = "Custom pizza",
            Price = 40,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Food")
        };

        context.Products.Add(product);

        await context.SaveChangesAsync();

        var breadGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 1,
            Name = "Bread"
        };

        product.OptionGroups.Add(breadGroup);

        var meatGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 2,
            Name = "Meat",
            Max = 2
        };

        product.OptionGroups.Add(meatGroup);

        var nonMeatGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 3,
            Name = "Non-Meat"
        };

        product.OptionGroups.Add(nonMeatGroup);

        var sauceGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 4,
            Name = "Sauce"
        };

        product.OptionGroups.Add(sauceGroup);

        var toppingsGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 5,
            Name = "Toppings"
        };

        product.OptionGroups.Add(toppingsGroup);

        await context.SaveChangesAsync();

        var optionStyle = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Style"
        };

        product.Options.Add(optionStyle);

        await context.SaveChangesAsync();

        var valueItalian = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Italian"
        };

        optionStyle.Values.Add(valueItalian);

        var valueAmerican = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "American"
        };

        optionStyle.DefaultValue = valueAmerican;

        optionStyle.Values.Add(valueAmerican);

        var optionHam = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Ham",
            Group = meatGroup,
            Price = 15
        };

        product.Options.Add(optionHam);

        var optionKebab = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Kebab",
            Group = meatGroup,
            Price = 10,
            IsSelected = true
        };

        product.Options.Add(optionKebab);

        var optionChicken = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Chicken",
            Group = meatGroup,
            Price = 10
        };

        product.Options.Add(optionChicken);

        var optionExtraCheese = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Extra cheese",
            Group = toppingsGroup,
            Price = 5
        };

        product.Options.Add(optionExtraCheese);

        var optionGreenOlives = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Green Olives",
            Group = toppingsGroup,
            Price = 5
        };

        product.Options.Add(optionGreenOlives);
    }

    public static async Task CreateSalad(ApplicationDbContext context)
    {
        var product = new Product()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sallad",
            Description = "Din egna sallad",
            Price = 52,
            Group = await context.ProductGroups.FirstAsync(x => x.Name == "Food"),
            Visibility = ProductVisibility.Listed
        };

        context.Products.Add(product);

        var baseGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 1,
            Name = "Bas"
        };

        product.OptionGroups.Add(baseGroup);

        var proteinGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 2,
            Name = "Välj protein",
            Max = 1
        };

        product.OptionGroups.Add(proteinGroup);

        var additionalGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 4,
            Name = "Välj tillbehör",
            Max = 3
        };

        product.OptionGroups.Add(additionalGroup);

        var dressingGroup = new OptionGroup()
        {
            Id = Guid.NewGuid().ToString(),
            Seq = 5,
            Name = "Välj dressing",
            Max = 1
        };

        product.OptionGroups.Add(dressingGroup);

        await context.SaveChangesAsync();

        var optionBase = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Bas",
            Group = baseGroup
        };

        product.Options.Add(optionBase);

        await context.SaveChangesAsync();

        var valueSallad = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sallad",
        };

        optionBase.Values.Add(valueSallad);

        var valueSalladPasta = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sallad med pasta"
        };

        optionBase.DefaultValue = valueSalladPasta;

        optionBase.Values.Add(valueSalladPasta);

        var valueSalladQuinoa = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sallad med quinoa",
        };

        optionBase.Values.Add(valueSalladQuinoa);

        var valueSalladNudlar = new OptionValue
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Sallad med glasnudlar",
        };

        optionBase.Values.Add(valueSalladNudlar);

        var optionChicken = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Kycklingfilé",
            Group = proteinGroup
        };

        product.Options.Add(optionChicken);

        var optionSmokedTurkey = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Rökt kalkonfilé",
            Group = proteinGroup
        };

        product.Options.Add(optionSmokedTurkey);

        var optionBeanMix = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Marinerad bönmix",
            Group = proteinGroup
        };

        product.Options.Add(optionBeanMix);

        var optionVegMe = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "VegMe",
            Group = proteinGroup
        };

        product.Options.Add(optionVegMe);

        var optionChevre = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Chevré",
            Group = proteinGroup
        };

        product.Options.Add(optionChevre);

        var optionSmokedSalmon = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Varmrökt lax",
            Group = proteinGroup
        };

        product.Options.Add(optionSmokedSalmon);

        var optionPrawns = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Handskalade räkor",
            Group = proteinGroup
        };

        product.Options.Add(optionPrawns);

        var optionCheese = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Parmesanost",
            Group = additionalGroup
        };

        product.Options.Add(optionCheese);

        var optionGreenOlives = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Gröna oliver",
            Group = additionalGroup
        };

        product.Options.Add(optionGreenOlives);

        var optionSoltorkadTomat = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Soltorkade tomater",
            Group = additionalGroup
        };

        product.Options.Add(optionSoltorkadTomat);

        var optionInlagdRödlök = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Inlagd rödlök",
            Group = additionalGroup
        };

        product.Options.Add(optionInlagdRödlök);

        var optionRostadAioli = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Rostad aioli",
            Group = dressingGroup
        };

        product.Options.Add(optionRostadAioli);

        var optionPesto = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Pesto",
            Group = dressingGroup
        };

        product.Options.Add(optionPesto);

        var optionOrtvinagret = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Örtvinägrett",
            Group = dressingGroup
        };

        product.Options.Add(optionOrtvinagret);

        var optionSoyavinagret = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Soyavinägrett",
            Group = dressingGroup
        };

        product.Options.Add(optionSoyavinagret);

        var optionRhodeIsland = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Rhode Island",
            Group = dressingGroup
        };

        product.Options.Add(optionRhodeIsland);

        var optionKimchimayo = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Kimchimayo",
            Group = dressingGroup
        };

        product.Options.Add(optionKimchimayo);

        var optionCaesar = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Caesar",
            Group = dressingGroup
        };

        product.Options.Add(optionCaesar);

        var optionCitronLime = new Option()
        {
            Id = Guid.NewGuid().ToString(),
            OptionType = OptionType.YesOrNo,
            Name = "Citronlime",
            Group = dressingGroup
        };

        product.Options.Add(optionCitronLime);
    }
}