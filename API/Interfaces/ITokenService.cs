using API.Entities;

namespace API.Interfaces;

public interface ITokenService
{
    //pass AppUser user to creaate token
    string CreateToken(AppUser user);
}
