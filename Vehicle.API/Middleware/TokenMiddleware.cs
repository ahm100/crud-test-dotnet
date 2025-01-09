using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Vehicle.API.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the Authorization header is present
            if (context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Headers["Authorization"].ToString();

                // Check if the token does not already start with "Bearer "
                if (!token.StartsWith("Bearer "))
                {
                    context.Request.Headers["Authorization"] = "Bearer " + token;
                }
            }

            await _next(context);
        }
    }

}
