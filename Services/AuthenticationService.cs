using System;
using LB_151.Data;

namespace LB_151.Models
{
    public class AuthenticationService
    {
        public string AuthenticationString { get; set; }
        private DateTime expires;
        private readonly IAdminSettings _settings;

        public AuthenticationService(IAdminSettings settings)
        {
            this._settings = settings;
        }

        // Returns either an authentication string or null if the authentication failed
        public string Authenticate(string password, string email)
        {
            // Todo fix app settings so this doesn't have to be hardcoded
            if (password.Equals("password") && email.Equals("admin@company.com"))
            {
                Random random = new Random();
                // AuthenticationString = random.RandomString(_settings.AuthenticationStringLength);
                AuthenticationString = random.RandomString(40);
                expires = DateTime.Now.AddMinutes(_settings.ExpirationTime);

                return AuthenticationString;
            }

            AuthenticationString = null;
            return AuthenticationString;
        }

        public bool IsValid(string authenticationString)
        {
            return AuthenticationString != null && AuthenticationString.Equals(authenticationString) &&
                   expires.CompareTo(DateTime.Now) <= 0;
        }
    }
}