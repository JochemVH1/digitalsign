using digitalsign.application.Contracts.V1.InputModels.User;
using digitalsign.application.Contracts.V1.ViewModels.User;
using System.Threading.Tasks;

namespace digitalsign.application.Services.Interface
{
    public interface IUserService
    {
        Task<UserViewModel> AddAsync(UserRegistrationInputModel model);
    }
}
