using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data.Interfaces
{
    public interface IApplicationUser
    {
        ApplicationUser GetById(string id);
        IEnumerable<ApplicationUser> GetAll();

        Task IncrementRating(string id);
        Task Add(ApplicationUser user);
        Task SetProfileImage(string id, Uri uri);
    }
}
