using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4;

namespace IdentityServer.IdentityServerSettings;

public static class IdentityServerConfiguration
{
    public static IEnumerable<Client> GetClients() =>
        new List<Client>
        {
            new Client
            {
                ClientId = "Api",
                ClientName = "Api",
                ClientSecrets = { new Secret("client_secret".ToSha256())},
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "api"
                }
            }
        };

    public static IEnumerable<ApiScope> GetApiScopes() =>
        new List<ApiScope>
        {
            new ApiScope
            {
                Name = "api",
                DisplayName = "api",
                Enabled = true,
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Address,
                    JwtClaimTypes.Confirmation,
                    JwtClaimTypes.EmailVerified,
                    JwtClaimTypes.Id,
                    JwtClaimTypes.Profile
                }
            }
        };

    public static IEnumerable<ApiResource> GetApiResources() =>
        new List<ApiResource>
        {
            new("api", "api") {Scopes = new List<string>{"api"}}
        };

    public static IEnumerable<IdentityResource> GetIdentityResources() =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email()
        };
}
