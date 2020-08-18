﻿namespace Infrastructure.Security.Models
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public bool Locked { get; set; }
    }
}
