using Microsoft.AspNetCore.SignalR;

namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob) 
    {   //get todays date
        var today = DateOnly.FromDateTime(DateTime.Now);

        //calculate age
        var age = today.Year - dob.Year;

        //take into account if they had their birthdate or not

        if(dob > today.AddYears(-age)) age--;
        
        return age;
    }
}
