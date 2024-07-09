using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController //add BaseApiController,  add ITokenService and call tokenService
{
    [HttpPost("register")] //account/register
    //inject RegisterDto and call it registerDto to pass username and password 
    public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)//task > action return a User after user has registered
    {
        //Add conditional passing UserExists() function and return BadRequest if username alrady exist
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        return Ok();

        //using var hmac = new HMACSHA512();//randomly generated key

        // var user = new AppUser///initialize properties of new AppUser
        // {
            // UserName = registerDto.Username.ToLower(),//Save in database as Tolower(), so we can later check that users can not use same username
            // PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),//Create byte [] from password passed
            // PasswordSalt = hmac.Key//Hash again to scramble twice the passwords
        // };

        // context.Users.Add(user);//add user to database 
        // await context.SaveChangesAsync();//save changes to database

        // return new UserDto //return new UserDto
        // {
            // Username = user.UserName,
            // Token = tokenService.CreateToken(user)
        // };
    }

    [HttpPost("login")]//create login endpoint
    //inject LoginDto and call it loginDto, get password hass to compare with the password the user is providing in login request
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)//task > action find the first user that matches a criteria or return NULL
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());
        //add conditional if user is null return "invalid user"
        if(user == null) return Unauthorized("Invalid username");
        //Compare password in database with the one they have provided, pass key PasswordSalt
        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));//Create byte [] from password passed to login
        ///loop through computer hash array Compare password in database with the one they have provided
        for (int i = 0; i < computedHash.Length; i++)
        {   //if password is NOT the same as the one  in database return Invalid password
            if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
        }
        //if they passwords match return user
        return new UserDto //return new UserDto
        {
            Username = user.UserName,
            Token = tokenService.CreateToken(user)
        };
    }


    //check database to see if we already have a username with the same username.
    private async Task<bool> UserExists(string username)//task > action bool True if username already exists
    {
        //chack if Any user with AnyAsync, this lambda will return true if username matches
        return await context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());//make them lower to unseure that it is a match
    }
}
