using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Common.AspNetCore;

public static class ClaimUtils
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        //if (principal == null)
        //    throw new ArgumentNullException(nameof(principal));

        //return (principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        var userIdString = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdString))
            throw new UnauthorizedAccessException("User is not authenticated or UserId claim is missing.");

        Guid.TryParse(userIdString, out var userId);
        return userId;
    }
    public static string GetPhoneNumber(this ClaimsPrincipal principal)
    {
        if (principal == null)
            throw new ArgumentNullException(nameof(principal));

        return principal.FindFirst(ClaimTypes.MobilePhone)?.Value;
    }
}