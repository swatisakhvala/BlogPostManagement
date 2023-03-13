using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using CommentService.Command;
using CommentService.Model;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

namespace CommentService
{
    public class AuthenticationToken
    {
        public async Task<bool> Validate(string token)
        {
            try
            {
                string myTenant = "51b500ed-4188-4de5-8778-335fa50483b3";
                var myAudience = "api://1ed5e9b3-09aa-4d83-a8dd-4060f1e0b32d";
                var myIssuer = String.Format(CultureInfo.InvariantCulture, "https://sts.windows.net/{0}/", myTenant);
                var mySecret = "upB8Q~RncORdyTfcpyMaZ8SfjhSvWlDJpiik0atu";
                var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
                var stsDiscoveryEndpoint = String.Format(CultureInfo.InvariantCulture, "https://login.microsoftonline.com/51b500ed-4188-4de5-8778-335fa50483b3/v2.0/.well-known/openid-configuration");


                var configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
                var config = await configManager.GetConfigurationAsync();

                var tokenHandler = new JwtSecurityTokenHandler();

                var validationParameters = new TokenValidationParameters
                {
                    ValidAudience = myAudience,
                    ValidIssuer = myIssuer,
                    IssuerSigningKeys = config.SigningKeys,
                    ValidateLifetime = false,
                    IssuerSigningKey = mySecurityKey
                };

                var validatedToken = (SecurityToken)new JwtSecurityToken();

                tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
                return true; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
