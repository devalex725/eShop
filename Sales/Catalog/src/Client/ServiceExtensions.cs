﻿using Microsoft.Extensions.DependencyInjection;

namespace YourBrand.Catalog.Client;

public static class ServiceExtensions
{
    public static IServiceCollection AddCatalogClients(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        services
            .AddCatalogClient(configureClient, builder)
            .AddOptionsClient(configureClient, builder)
            .AddAttributesClient(configureClient, builder)
            .AddStoresClient(configureClient, builder)
            .AddBrandsClient(configureClient, builder)
            .AddCurrenciesClient(configureClient, builder);

        return services;
    }

    public static IServiceCollection AddCatalogClient(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        var b = services
            .AddHttpClient(nameof(ProductsClient), configureClient)
            .AddTypedClient<IProductsClient>((http, sp) => new ProductsClient(http));

        builder?.Invoke(b);

        var b2 = services
            .AddHttpClient(nameof(ProductGroupsClient), configureClient)
            .AddTypedClient<IProductGroupsClient>((http, sp) => new ProductGroupsClient(http));

        builder?.Invoke(b2);

        return services;
    }

    public static IServiceCollection AddOptionsClient(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        var b = services
            .AddHttpClient(nameof(OptionsClient), configureClient)
            .AddTypedClient<IOptionsClient>((http, sp) => new OptionsClient(http));

        builder?.Invoke(b);

        return services;
    }

    public static IServiceCollection AddAttributesClient(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        var b = services
            .AddHttpClient(nameof(AttributesClient), configureClient)
            .AddTypedClient<IAttributesClient>((http, sp) => new AttributesClient(http));

        builder?.Invoke(b);

        return services;
    }

    public static IServiceCollection AddStoresClient(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        var b = services
            .AddHttpClient(nameof(StoresClient), configureClient)
            .AddTypedClient<IStoresClient>((http, sp) => new StoresClient(http));

        builder?.Invoke(b);

        return services;
    }

    public static IServiceCollection AddBrandsClient(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        var b = services
            .AddHttpClient(nameof(BrandsClient), configureClient)
            .AddTypedClient<IBrandsClient>((http, sp) => new BrandsClient(http));

        builder?.Invoke(b);

        return services;
    }

    public static IServiceCollection AddCurrenciesClient(this IServiceCollection services, Action<IServiceProvider, HttpClient> configureClient, Action<IHttpClientBuilder>? builder = null)
    {
        var b = services
            .AddHttpClient(nameof(CurrenciesClient), configureClient)
            .AddTypedClient<ICurrenciesClient>((http, sp) => new CurrenciesClient(http));

        builder?.Invoke(b);

        return services;
    }
}