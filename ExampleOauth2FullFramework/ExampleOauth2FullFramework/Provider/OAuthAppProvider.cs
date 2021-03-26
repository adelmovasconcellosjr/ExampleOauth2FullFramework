using ExampleOauth2FullFramework.Models;
using ExampleOauth2FullFramework.Service;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ExampleOauth2FullFramework.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                var username = context.UserName;
                var password = context.Password;
                var userService = new UserService();
                try
                {
                    UserApp user = userService.GetUserByCredentials(username, password);
                    if (user != null)
                    {
                        var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim("UserID", user.Id.ToString())
                    };

                        ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                        context.Validated(new AuthenticationTicket(oAutIdentity, new AuthenticationProperties() { }));
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Error");
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Equals("Nome de usuário ou senha incorretos.\r\n"))
                    {
                        context.SetError("invalid_grant", ex.Message.Replace("\r\n", ""));
                    }
                    else
                    {
                        context.SetError("invalid_grant", ex.Message);
                    }
                }
            });
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}