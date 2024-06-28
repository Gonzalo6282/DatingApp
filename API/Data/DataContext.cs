using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

//Create class DataContext > : DbContext to import EntityFrameworkCore > pass options
public class DataContext(DbContextOptions options) : DbContext(options)
{
    //Set database > passing AppUser from Entities > call new table Users
    public DbSet<AppUser> Users { get; set; }
}
