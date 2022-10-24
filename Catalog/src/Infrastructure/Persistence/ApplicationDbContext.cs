﻿using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using YourBrand.Catalog.Domain.Entities;
using YourBrand.Catalog.Infrastructure.Persistence.Interceptors;
using YourBrand.Catalog.Infrastructure.Persistence.Outbox;

namespace YourBrand.Catalog.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ApplySoftDeleteQueryFilter(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    private static void ApplySoftDeleteQueryFilter(ModelBuilder modelBuilder)
    {
        // INFO: This code adds a query filter to any object deriving from Entity
        //       and that is implementing the ISoftDelete interface.
        //       The generated expressions correspond to: (e) => e.Deleted == null.
        //       Causing the entity not to be included in the result if Deleted is not null.
        //       There are other better ways to approach non-destructive "deletion".

        var entityBaseType = typeof(Entity);
        var softDeleteInterface = typeof(ISoftDelete);
        var deletedProperty = softDeleteInterface.GetProperty(nameof(ISoftDelete.Deleted));

        foreach (var entityType in softDeleteInterface.Assembly
            .GetTypes()
            .Where(candidateEntityType => entityBaseType.IsAssignableFrom(candidateEntityType))
            .Where(candidateEntityType => softDeleteInterface.IsAssignableFrom(candidateEntityType)))
        {
            var param = Expression.Parameter(entityType, "entity");
            var body = Expression.Equal(Expression.Property(param, deletedProperty!), Expression.Constant(null));
            var lambda = Expression.Lambda(body, param);

            modelBuilder.Entity(entityType).HasQueryFilter(lambda);
        }
    }

#nullable disable

    public DbSet<ItemGroup> ItemGroups { get; set; } = null!;

    public DbSet<Item> Items { get; set; } = null!;

    public DbSet<ItemAttribute> ItemAttributes { get; set; } = null!;

    public DbSet<AttributeGroup> AttributeGroups { get; set; } = null!;

    public DbSet<Domain.Entities.Attribute> Attributes { get; set; } = null!;

    public DbSet<AttributeValue> AttributeValues { get; set; } = null!;

    public DbSet<ItemOption> ItemOptions { get; set; } = null!;

    public DbSet<ItemAttributeValue> ItemAttributeValues { get; set; } = null!;

    public DbSet<ItemVariantOption> ItemVariantOptions { get; set; } = null!;

    public DbSet<OptionGroup> OptionGroups { get; set; } = null!;

    public DbSet<Option> Options { get; set; } = null!;

    public DbSet<OptionValue> OptionValues { get; set; } = null!;


#nullable restore
}
