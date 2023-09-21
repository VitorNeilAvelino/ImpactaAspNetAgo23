using Microsoft.AspNetCore.Identity;

namespace ExpoCenter.Mvc.App_Start
{
    public class IdentityConfig
    {
        public static Action<IdentityOptions> SetIdentityOptions()
        {
            return options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
               
                options.Password.RequiredLength = 3;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
            };
        }
    }
}