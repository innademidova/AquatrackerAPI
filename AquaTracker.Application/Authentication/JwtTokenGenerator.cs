using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AquaTracker.Application.Common.Interfaces;
using AquaTracker.Domain.Users;
using Microsoft.IdentityModel.Tokens;

namespace AquaTracker.Application.Authentication;

public class JwtTokenGenerator: IJwtTokenGenerator
{
    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(AuthOptions.Key)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
        };

        var securityToken = new JwtSecurityToken(
            issuer: AuthOptions.Issuer,
            audience: AuthOptions.Audience,
            expires: DateTime.UtcNow.AddMinutes(30),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}