using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Api.Security;

/// <summary>
/// Filtro personalizado para validar roles específicos en endpoints.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public sealed class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _rolesPermitidos;

    public AuthorizeRoleAttribute(params string[] roles)
    {
        _rolesPermitidos = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var rolClaim = context.HttpContext.User.FindFirst(ClaimTypes.Role);
        if (rolClaim is null || !_rolesPermitidos.Contains(rolClaim.Value, StringComparer.OrdinalIgnoreCase))
        {
            context.Result = new ForbidResult();
        }
    }
}
