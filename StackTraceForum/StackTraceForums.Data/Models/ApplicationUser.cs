﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Forum.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int Rating { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime MemberSincey { get; set; }
        public bool isActive { get; set; }
    }
}
