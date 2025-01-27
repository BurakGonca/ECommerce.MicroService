using Microsoft.AspNetCore.Identity;

namespace ECommerce.IdentityServer.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //Identity genisletilmesi
        public string Name { get; set; }
        public string Surname { get; set; }



    }
}
