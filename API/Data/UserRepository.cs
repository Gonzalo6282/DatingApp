using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//inject Datacontext and call it context because we are going to do databse queries
public class UserRepository(DataContext context, IMapper mapper) : IUserRepository //Use IUserRepository interface
{
    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .ToListAsync();

    }
    public async Task<AppUser?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);//returns user by id
    }

    public async Task<AppUser?> GetUserByUsernameAsync(string username)
    {
        return await context.Users
        .Include(x => x.Photos)//include photos 
        .SingleOrDefaultAsync(x => x.UserName == username);//returns user by username
    }

    public async Task<IEnumerable<AppUser>> GetUsersAsync()//IEnumerable = collection. like a list
    {
        return await context.Users
        .Include(x => x.Photos)//include photos 
        .ToListAsync();//returns a list 
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;//returns int, number of changes saved into db. If greater than 0 means that someting has been saved so return true
    }

    public void Update(AppUser user)
    {
        context.Entry(user).State = EntityState.Modified;//if user state is modified tells entityframework
    }
}
