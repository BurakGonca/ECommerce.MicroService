﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ECommerce.IdentityServer
{
    public static class Config
    {
        



        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("ResourceCatalog")
            {
                Scopes = {"CatalogFullPermission","CatalogReadPermission"}
            },

            new ApiResource("ResourceDiscount")
            {
                Scopes= {"DiscountFullPermission"}
            },

            new ApiResource("ResourceOrder")
            {
                Scopes= {"OrderFullPermission"}
            },

            new ApiResource("ResourceCargo")
            {
                Scopes= {"CargoFullPermission"}
            },

            new ApiResource("ResourceBasket")
            {
                Scopes= {"BasketFullPermission"}
            },

            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)


        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("CatalogFullPermission","Full Authority For Catalog Operations"),
            new ApiScope("CatalogReadPermission","Reading Authority For Catalog Operations"),
            new ApiScope("DiscountFullPermission","Full Authority For Discount Operations"),
            new ApiScope("OrderFullPermission","Full Authority For Order Operations"),
            new ApiScope("CargoFullPermission","Full Authority For Cargo Operations"),
            new ApiScope("BasketFullPermission","Full Authority For Basket Operations"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)

        };


        public static IEnumerable<Client> Clients => new Client[]
        {
            //Visitor kullanicisi icin
            new Client
            {
                ClientId="ECommerceVisitorId",
                ClientName="E-Commerce Visitor User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = { "CatalogReadPermission" }
            },

            //Manager kullanicisi icin
            new Client
            {
                ClientId="ECommerceManagerId",
                ClientName="E-Commerce Manager User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"CatalogReadPermission", "CatalogFullPermission" , "BasketFullPermission" }
            },

            //Admin kullanicisi icin
            new Client
            {
                ClientId="ECommerceAdminId",
                ClientName="E-Commerce Admin User",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"CatalogReadPermission", "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermission","CargoFullPermission","BasketFullPermission",

                    IdentityServerConstants.LocalApi.ScopeName,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile

                },
                AccessTokenLifetime = 600 //10dk gecerli olacak Token

                
            }

        };



    }
}