﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Forum.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserDescription { get; set; }
        public int Rating { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSince { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
    }
}
