using Microsoft.AspNetCore.Authentication;

namespace UsersTest.Auth
{
    public class BasicAuthOptions : AuthenticationSchemeOptions
    {
        public string Login { get; set; }
        public string Pass { get; set; }
    }
}