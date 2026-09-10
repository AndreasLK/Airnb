using Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace Domain.Entities
{
    public class User : AggregateRoot
    {
    public User() { }

        public string AuthenticationJWT { get; private set; }

        public string Password { get; private set; }
    }
}
