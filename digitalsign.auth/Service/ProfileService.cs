using digitalsign.auth.Models;
using digitalsign.common.Enumeration;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

public class ProfileService : IProfileService
{
    protected UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        //>Processing
        var user = await _userManager.GetUserAsync(context.Subject);

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Role, Enum.Parse(typeof(UserRole), user.Role.ToString()).ToString(), ClaimValueTypes.String),
            new Claim(JwtClaimTypes.FamilyName, user.LastName.ToString(), ClaimValueTypes.String),
            new Claim(JwtClaimTypes.GivenName, user.FirstName.ToString(), ClaimValueTypes.String),
            new Claim(JwtClaimTypes.Email, user.Email.ToString(), ClaimValueTypes.String),
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        //>Processing
        var user = await _userManager.GetUserAsync(context.Subject);

        context.IsActive = (user != null);
    }
}