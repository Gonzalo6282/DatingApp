using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;

//inject IConfiguration and call config
public class TokenService(IConfiguration config) : ITokenService
{
    public string CreateToken(AppUser user)
    {
        //If tokenkey = NULL throw exception
        var tokenKey = config["TokenKey"] ?? throw new Exception("Cannot access tokenkey from appsettings");
        //if tokenkey length is short throw exception
        if(tokenKey.Length < 64) throw new Exception("Your tokenkey needs to be longer");
        //create key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
       //cretae claim > new list of claims
       var claims = new List<Claim>
       {
        new(ClaimTypes.NameIdentifier, user.UserName)
       };

       //create creds > pass key > pass security algorithm to encrypt key
       var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

       //create token descriptor
       var tokenDescriptor = new SecurityTokenDescriptor
       {
        Subject = new ClaimsIdentity(claims),//create subject
        Expires = DateTime.UtcNow.AddDays(7),//create expiry date
        SigningCredentials = creds//create credentials
       };

       var tokenHandler =  new JwtSecurityTokenHandler();//create tokenHandler
       var token = tokenHandler.CreateToken(tokenDescriptor);//create token with tokenHandler

       return tokenHandler.WriteToken(token);//return token

    }
}
