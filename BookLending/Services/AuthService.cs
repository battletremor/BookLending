using BookLending.Interfaces;
using static BookLending.Repositories.AuthenticationRepository;

namespace BookLending.Services
{
    public class AuthService : IAuthenticationService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthService(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        public int Login(string Email, string password)
        {
            return _authenticationRepository.Login(Email, password);
        }

        public int Register(RegisterInfo regInfo)
        {
            return _authenticationRepository.Register(regInfo);
        }

    }
}
