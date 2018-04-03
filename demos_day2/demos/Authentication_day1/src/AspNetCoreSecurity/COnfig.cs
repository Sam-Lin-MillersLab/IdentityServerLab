using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace AspNetCoreSecurity
{
    public class Config
    {
        public static List<Client> Clients = new List<Client> {
            new Client
            {
                ClientId= "client1",
                ClientName = "Sample Client 1",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { "http://localhost:2560/Account/Callback" },
                RequireConsent = false,
                AllowedScopes = { "openid", "profile", "officeInfo" }
            },
            new Client
            {
                ClientId= "client2",
                ClientName = "Sample Client 2",
                AllowedGrantTypes = GrantTypes.Implicit,
                RedirectUris = { "http://localhost:2563/signin-oidc" },
                RequireConsent = false,
                AllowedScopes = { "openid", "profile", "officeInfo" }
            },
        };

        public static List<IdentityResource> IdentityResources = new List<IdentityResource> {
            new IdentityResources.OpenId(),
            //new IdentityResource("openid", new[]{ "sub" }),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResources.Phone(),
            new IdentityResource("officeInfo", new[]{ "officePhone", "officeLocation" })
        };
    }
}
