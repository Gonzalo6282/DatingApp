using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
//swap Datacontext context for IUserRepository userRepository
[Authorize]//authenticate all http endpoints
public class UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService) : BaseApiController //add BaseApiController and  inject DataContext class API.Data.DataContext 
{
    //action return a list. List IEnumerable of type AppUSer > give enpoint a name "GetUsers()" 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>>GetUsers([FromQuery]UserParams userParams) //add asyn ans Task<>, to make async code. action return a list
    {
        userParams.CurrentUsername = User.GetUsername();
        //call list of users from databse and add to variable
        var users = await userRepository.GetMembersAsync(userParams);//pass user repository to get mebers

        Response.AddPaginationHeader(users);
        // return list of users to client
        return Ok(users);
    }
     [HttpGet("{username}")] // api/users/id
     public async Task<ActionResult<MemberDto>>GetUser(string username) //Get user username. //add asyn ans Task<>, to make async code
    {
     //call list of user from databse and add to variable
     var user = await userRepository.GetMemberAsync(username);//add await and Async to Find, to make async code
     //Check if user is in database
     if(user==null) return NotFound();

     // return list of user username to client
     return user;
    }

    [HttpPut]//update user
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        var user =  await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Could not find user");

        mapper.Map(memberUpdateDto, user);

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update the user");
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var user =  await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Cannot update the user");

        var result = await photoService.AddPhotoAsync(file);

        if (result.Error != null) return BadRequest(result.Error.Message);

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        if(user.Photos.Count == 0) photo.IsMain = true;

        user.Photos.Add(photo);

        if (await userRepository.SaveAllAsync()) return CreatedAtAction(nameof(GetUser),new {username = user.UserName}, mapper.Map<PhotoDto>(photo));

        return BadRequest("Problem adding photo");

    }

    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("Could not find user");

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null || photo.IsMain) return BadRequest("Cannot use this as main photo");

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null) currentMain.IsMain = false;
        photo.IsMain = true;

        if (await userRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Problem setting main photo");
    }


    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        var user = await userRepository.GetUserByUsernameAsync(User.GetUsername());

        if (user == null) return BadRequest("User not found");

        var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

        if (photo == null || photo.IsMain) return BadRequest("This photo cannot be deleted");

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);
            if (result.Error != null) return BadRequest(result.Error.Message);
        }

        user.Photos.Remove(photo);

        if (await userRepository.SaveAllAsync()) return Ok();

        return BadRequest("Problem deleting photo");
    }

}

