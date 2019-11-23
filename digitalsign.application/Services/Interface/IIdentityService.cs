using System.Threading.Tasks;
using digitalsign.application.Contracts.V1.Autherization.ApplicationUser;
using digitalsign.application.Contracts.V1.ViewModels.ApplicationUser;
using digitalsign.common.ApiResult;

namespace digitalsign.application.Services.Interface
{
    public interface IIdentityService
    {
        Task<ApiResult<ApplicationUserViewModel>> RegisterAsync(ApplicationUserCreateModel model);
        Task<ApiResult<ApplicationUserViewModel>> LoginAsync(ApplicationUserLoginModel model);
        Task<ApiResult<ApplicationUserViewModel>> RefreshAsync(ApplicationUserRefreshModel refreshModel);
    }
}
