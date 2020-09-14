using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaAPI.IntegrationTest.Servicios
{
    public class SaltarRequerimientosJWT : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var item in context.Requirements.ToList())
            {
                context.Succeed(item);
            }
            return Task.CompletedTask;
        }
    }
}
