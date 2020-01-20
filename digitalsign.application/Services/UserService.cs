using digitalsign.application.Contracts.V1.InputModels.User;
using digitalsign.application.Contracts.V1.ViewModels.User;
using digitalsign.application.Services.Interface;
using digitalsign.domain.Domain;
using digitalsign.persistence.Context;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace digitalsign.application.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(
            ApplicationDbContext context,
            IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserViewModel> AddAsync(UserRegistrationInputModel userInputModel)
        {
            if (userInputModel == null) throw new ArgumentNullException(nameof(userInputModel));

            var newUser = new User
            {
                FirstName = userInputModel.FirstName,
                LastName = userInputModel.LastName,
                Id = Guid.NewGuid(),
                Identity = userInputModel.Identity,
                Role = userInputModel.Role
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return new UserViewModel
            {
                FirstName = userInputModel.FirstName,
                LastName = userInputModel.LastName,
                Identity = userInputModel.Identity,
                Role = userInputModel.Role
            };
        }
    }
}
