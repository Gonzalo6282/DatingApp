using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

//call database table Photos, by default would take the public class singular name Photo 
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public bool IsMain { get; set; }
    public string? PublicId { get; set; }

    //navigation properties, create a required one to many relationships
    public int AppUserId { get; set; }

    public AppUser AppUser { get; set; } = null!;
}