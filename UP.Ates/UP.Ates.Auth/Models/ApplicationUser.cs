using Microsoft.AspNetCore.Identity;

namespace UP.Ates.Auth.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
    }
    
    public class ApplicationUserContract : ApplicationUser
    {
        public string Password { get; set; }
    }
}
