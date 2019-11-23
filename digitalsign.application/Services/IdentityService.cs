using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.Autherization.ApplicationUser;
using digitalsign.application.Contracts.V1.ViewModels.ApplicationUser;
using digitalsign.application.Security.Interface;
using digitalsign.application.Services.Interface;
using digitalsign.common.ApiResult;
using digitalsign.common.Enumeration;
using digitalsign.persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace digitalsign.application.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public IdentityService(UserManager<ApplicationUser> userManager, IJwtTokenHandler jwtTokenHandler)
        {
            _userManager = userManager;
            _jwtTokenHandler = jwtTokenHandler;
        }

        public async Task<ApiResult<ApplicationUserViewModel>> LoginAsync(ApplicationUserLoginModel model)
        {
            var apiResult = new ApiResult<ApplicationUserViewModel>();
            try
            {
                var user = await _userManager.FindByEmailAsync(model.username);
                if(user == null) 
                {
                    apiResult.StatusCode = StatusCode.Unauthorized;
                    apiResult.Notifications.Add($"User with email: {model.username} doesn't not exist.");
                } 
                else 
                {
                    var hasValidPassword = await _userManager.CheckPasswordAsync(user, model.password);
                    if(!hasValidPassword) 
                    {
                        apiResult.StatusCode = StatusCode.Unauthorized;
                        apiResult.Notifications.Add("Incorrect password.");
                    } 
                    else 
                    {
                        var result = _jwtTokenHandler.BuildJWT(user);
                        user.RefreshToken = result.refreshToken;
                        await _userManager.UpdateAsync(user);
                        apiResult.Success = true;
                        apiResult.StatusCode = StatusCode.Successful;
                        apiResult.ContainingObject = new ApplicationUserViewModel 
                        {
                            Token = result.jwtToken,
                            RefreshToken = result.refreshToken.Token
                        };
                    }
                }
            }
            catch (System.Exception ex)
            {
                apiResult.Notifications.Add($"failed to login. innerexception: {ex.Message}");
            }
            return apiResult;
        }

        public async Task<ApiResult<ApplicationUserViewModel>> RefreshAsync(ApplicationUserRefreshModel refreshModel)
        {
            var apiResult = new ApiResult<ApplicationUserViewModel>();
            try
            {
                var validatedToken = _jwtTokenHandler.GetClaimsPrincipalFromToken(refreshModel.Token);
                if (validatedToken == null) 
                {
                    apiResult.StatusCode = StatusCode.Unauthorized;
                    apiResult.Notifications.Add("Invalid token");
                }
                else
                {
                    var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
                    var expiryDateTimeUtc = new DateTime(1970,1,1,0,0,0).AddSeconds(expiryDateUnix);

                    if (expiryDateTimeUtc > DateTime.UtcNow)
                    {
                        apiResult.StatusCode = StatusCode.Unrefreshable;
                        apiResult.Success = true;
                        apiResult.ContainingObject = new ApplicationUserViewModel {
                            Token = refreshModel.Token,
                            RefreshToken = refreshModel.RefreshToken
                        };
                        apiResult.Notifications.Add("No need to refresh");
                    }
                    else
                    {   
                        var principalId = validatedToken.Claims.Single(claim => claim.Type == "id").Value;          
                        var user = await _userManager.Users
                        .Include(x => x.RefreshToken)
                        .SingleOrDefaultAsync(x => x.Id.Equals(principalId));

                         if (!user.RefreshToken.Token.Equals(refreshModel.RefreshToken) || 
                            DateTime.UtcNow > user.RefreshToken.ExpiryDate || 
                            user.RefreshToken.Invalidated) 
                        {
                            apiResult.StatusCode = StatusCode.Unauthorized;
                            apiResult.Notifications.Add("Invalid refresh token");
                        }
                        else
                        {

                            var result = _jwtTokenHandler.BuildJWT(user);
                            user.RefreshToken = result.refreshToken;
                            await _userManager.UpdateAsync(user);
                            apiResult.Success = true;
                            apiResult.StatusCode = StatusCode.Successful;
                            apiResult.ContainingObject = new ApplicationUserViewModel 
                            {
                                Token = result.jwtToken,
                                RefreshToken = result.refreshToken.Token
                            };
                        } 
                    }
                }
            }
            catch (System.Exception ex)
            {
                apiResult.Notifications.Add($"failed to register. innerexception: {ex.Message}");
            }

            return apiResult;
        }

        public async Task<ApiResult<ApplicationUserViewModel>> RegisterAsync(ApplicationUserCreateModel model)
        {
            var apiResult = new ApiResult<ApplicationUserViewModel>();
            try
            {
                var existingUser = await _userManager.FindByEmailAsync(model.username);
                if(existingUser != null) 
                {
                    apiResult.StatusCode = StatusCode.Unauthorized;
                    apiResult.Notifications.Add($"User with email: {model.username} already exists.");
                } else {
                    var newUser = new ApplicationUser 
                    {
                        Email = model.username,
                        UserName = model.username
                    };
                    var createdUser = await  _userManager.CreateAsync(newUser, model.password);
                    if(!createdUser.Succeeded)
                    {
                        apiResult.StatusCode = StatusCode.Unauthorized;
                        apiResult.Notifications.Add($"Failed to create user with email: {newUser.Email}.");
                        foreach (string error in createdUser.Errors.Select(x => $"{x.Code}: {x.Description}"))
                        {
                            apiResult.Notifications.Add(error);
                        }
                    } 
                    else 
                    {                       
                        var result = _jwtTokenHandler.BuildJWT(newUser);
                        newUser.RefreshToken = result.refreshToken;
                        await _userManager.UpdateAsync(newUser);
                        apiResult.Success = true;
                        apiResult.StatusCode = StatusCode.Successful;
                        apiResult.ContainingObject = new ApplicationUserViewModel 
                        {
                            Token = result.jwtToken,
                            RefreshToken = result.refreshToken.Token
                        };
                    }
                }
            }
            catch (System.Exception ex)
            {
                apiResult.Notifications.Add($"failed to register. innerexception: {ex.Message}");
            }
            return apiResult;
        }
    }
}
