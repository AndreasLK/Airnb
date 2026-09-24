using Airnb.Domain.Common;

namespace Airnb.Domain.Entities
{
    public class User : AggregateRoot
    {
        public User() { }

        public string AuthenticationJWT { get; private set; }

        public string Password { get; private set; }

        public User(string authenticationJWT, string password)
        {
            AuthenticationJWT = authenticationJWT;
            Password = password;
        }
    }
}
