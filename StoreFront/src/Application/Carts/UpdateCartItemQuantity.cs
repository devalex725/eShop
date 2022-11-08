﻿using MediatR;
using YourBrand.Catalog;
using YourBrand.Sales;

namespace YourBrand.StoreFront.Application.Carts;

public sealed record UpdateCartItemQuantity(string Id, int Quantity) : IRequest
{
    sealed class Handler : IRequestHandler<UpdateCartItemQuantity>
    {
        private readonly ICartsClient cartsClient;
        private readonly IItemsClient itemsClient;
        private readonly ICartHubService cartHubService;
        private readonly ICurrentUserService currentUserService;

        public Handler(
            YourBrand.Sales.ICartsClient cartsClient,
            YourBrand.Catalog.IItemsClient itemsClient,
            ICartHubService cartHubService,
            ICurrentUserService currentUserService)
        {
            this.cartsClient = cartsClient;
            this.itemsClient = itemsClient;
            this.cartHubService = cartHubService;
            this.currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateCartItemQuantity request, CancellationToken cancellationToken)
        {
            var customerId = currentUserService.CustomerNo;
            var clientId = currentUserService.ClientId;

            string tag = customerId is null ? $"cart-{clientId}" : $"cart-{customerId}";

            var cart = await cartsClient.GetCartByTagAsync(tag, cancellationToken);

            await cartsClient.UpdateCartItemQuantityAsync(cart.Id, request.Id, request.Quantity, cancellationToken);

            await cartHubService.UpdateCart();

            return Unit.Value;
        }
    }
}

