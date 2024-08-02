using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(AppUser user);
    //when saving changes into databse return a boolean to say if thing have changed
    Task<bool> SaveAllAsync();
    //return a collection of user , like a list
    Task<IEnumerable<AppUser>> GetUsersAsync();
    //return a user by id, make optional with ? so can return null if wont find user by id
    Task<AppUser?> GetUserByIdAsync(int id);
    //return user by username, make optional with ? so can return null if wont find user by username
    Task<AppUser?> GetUserByUsernameAsync(string username);
    //return collection of MemberDto and call it GetMemberAsync
    Task<PagedList<MemberDto>> GetMembersAsync(UserParams userParams);
    //return MemberDto by username
    Task<MemberDto?> GetMemberAsync(string username);

}
