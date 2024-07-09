using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
//swap Datacontext context for IUserRepository userRepository
[Authorize]//authenticate all http endpoints
public class UsersController(IUserRepository userRepository) : BaseApiController //add BaseApiController and  inject DataContext class API.Data.DataContext 
{
    //action return a list. List IEnumerable of type AppUSer > give enpoint a name "GetUsers()" 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>>GetUsers() //add asyn ans Task<>, to make async code. action return a list
    {
        //call list of users from databse and add to variable
        var users = await userRepository.GetMembersAsync();//pass user repository to get mebers
        // return list of users to client
        return Ok(users);
    }
     [HttpGet("{username}")] // api/users/id
     public async Task<ActionResult<MemberDto>>GetUsers(string username) //Get user username. //add asyn ans Task<>, to make async code
    {
     //call list of user from databse and add to variable
     var user = await userRepository.GetMemberAsync(username);//add await and Async to Find, to make async code
     //Check if user is in database
     if(user==null) return NotFound();

     // return list of user username to client
     return user;
    }

}

