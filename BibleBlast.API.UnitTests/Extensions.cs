using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleBlast.API.UnitTests
{
    internal static class Extensions
    {
        public static void AddUserClaim(this ControllerBase controller, string type, object value)
        {
            if (controller.User == null)
            {
                throw new System.ArgumentException(nameof(controller.User));
            }

            controller.User.AddIdentity(new ClaimsIdentity(new[] { new Claim(type, value.ToString()) }));
        }
    }
}
