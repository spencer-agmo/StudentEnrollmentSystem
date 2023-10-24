
using System;
using Microsoft.AspNetCore.Identity;

namespace StudentEnrollmentSystem.Models.Auth
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}