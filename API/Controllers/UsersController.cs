﻿using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class UsersController(DataContext context) : BaseApiController //add BaseApiController and  inject DataContext class API.Data.DataContext 
{
    
    //action return a list. List IEnumerable of type AppUSer > give enpoint a name "GetUsers()" 
    [AllowAnonymous]//add AllowAnonymous data annotaion 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>>GetUsers() //add asyn ans Task<>, to make async code. action return a list
    {
        //call list of users from databse and add to variable
        var users = await context.Users.ToListAsync();//add await and Async to ToList, to make async code
        // return list of users to client
        return users;
    }

     [Authorize]//add Authorize data annotaion 
     [HttpGet("{id:int}")] // api/users/id
     public async Task<ActionResult<AppUser>>GetUsers(int id) //Get user id. //add asyn ans Task<>, to make async code
 {
     //call list of user from databse and add to variable
     var user = await context.Users.FindAsync(id);//add await and Async to Find, to make async code

     //Check if user is in database
     if(user==null) return NotFound();

     // return list of user id to client
     return user;
 }

}

