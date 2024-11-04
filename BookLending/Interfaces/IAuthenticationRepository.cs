using static BookLending.Repositories.AuthenticationRepository;

namespace BookLending.Interfaces
{
    public interface IAuthenticationRepository
    {
        int Register(RegisterInfo regInfo);

        int Login(string Email, string Password);
    }
}
