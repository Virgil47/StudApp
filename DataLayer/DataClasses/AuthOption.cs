using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataClasses
{
    public class AuthOption
    {
        public const string ISSUER = "StudApp";
        public const string AUDIENCE = "client";
        const string KEY = "SirPmJfn6rOM1bK9iV81";
        public const int LIFETIME = 1;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
