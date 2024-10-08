﻿using System.Security.Claims;
using AquaTracker.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AquaTracker.Infrastructure.Middlewares;

public class SetupUserClaimsMiddleware
{
    private readonly RequestDelegate _next;

    public SetupUserClaimsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ICurrentUser currentUser)
    {
        var user = context.User;

        if (user.Identity?.IsAuthenticated == false)
        {
            await _next(context);
        }
        else
        {
            var userIdFromToken = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var id = int.Parse(userIdFromToken ?? throw new InvalidOperationException("Can not retrieve user id from token"));
            var email = user.FindFirstValue(ClaimTypes.Email);
            
            currentUser.Id = id;
            currentUser.Email = email!;

            await _next(context);
        }
    }
}