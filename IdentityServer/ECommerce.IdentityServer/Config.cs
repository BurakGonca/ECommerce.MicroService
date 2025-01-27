// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace ECommerce.IdentityServer
{
    public static class Config
    {
        //public static IEnumerable<IdentityResource> IdentityResources =>
        //           new IdentityResource[]
        //           {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //           };

        //public static IEnumerable<ApiScope> ApiScopes =>
        //    new ApiScope[]
        //    {
        //        new ApiScope("scope1"),
        //        new ApiScope("scope2"),
        //    };

        //public static IEnumerable<Client> Clients =>
        //    new Client[]
        //    {
        //        // m2m client credentials flow client
        //        new Client
        //        {
        //            ClientId = "m2m.client",
        //            ClientName = "Client Credentials Client",

        //            AllowedGrantTypes = GrantTypes.ClientCredentials,
        //            ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

        //            AllowedScopes = { "scope1" }
        //        },

        //        // interactive client using code flow + pkce
        //        new Client
        //        {
        //            ClientId = "interactive",
        //            ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

        //            AllowedGrantTypes = GrantTypes.Code,

        //            RedirectUris = { "https://localhost:44300/signin-oidc" },
        //            FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
        //            PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

        //            AllowOfflineAccess = true,
        //            AllowedScopes = { "openid", "profile", "scope2" }
        //        },
        //    };



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
                AllowedScopes = {"CatalogReadPermission"}
            },

            //Manager kullanicisi icin
            new Client
            {
                ClientId="ECommerceManagerId",
                ClientName="E-Commerce Manager User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"CatalogReadPermission", "CatalogFullPermission" }
            },

            //Admin kullanicisi icin
            new Client
            {
                ClientId="ECommerceAdminId",
                ClientName="E-Commerce Admin User",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"CatalogReadPermission", "CatalogFullPermission", "DiscountFullPermission", "OrderFullPermission",

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