using System;
using System.Collections.Generic;
using System.Text;

namespace Airnb.Application.Security.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
