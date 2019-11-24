using Forum.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Data
{
    public interface IForum
    {
        Models.Forum GetById(int id);
        IEnumerable<Models.Forum> GetAll();
        IEnumerable<ApplicationUser> GetApplicationUsers();

        Task Create(Models.Forum forum);
        Task Delete(int forumId);
        Task UpdateForumTitle(int forumId, string newTitle);
        Task UpdateForumDescription(int forumId, string newDescription);
    }
}
