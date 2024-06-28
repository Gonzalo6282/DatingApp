namespace API.Entities;

public class AppUser
{
    //Create properrties to create table in database
    public int Id { get; set; }
    public required string UserName { get; set; }

}
