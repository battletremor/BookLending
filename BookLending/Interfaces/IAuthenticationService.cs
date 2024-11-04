using static BookLending.Repositories.AuthenticationRepository;

namespace BookLending.Interfaces
{
    public interface IAuthenticationService
    {
        int Login(string Email, string password);

        int Register(RegisterInfo regInfo);
    }
}
