using Microsoft.EntityFrameworkCore;
using Forum.Data;
using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Forum.Service
{
    public class ForumService : IForum
    {
        private readonly ApplicationDbContext _context;

        public ForumService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Create(Data.Models.Forum forum)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int forumId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Data.Models.Forum> GetAll()
        {
            return _context.Forums
                .Include(f => f.Posts);
        }

        public IEnumerable<ApplicationUser> GetApplicationUsers()
        {
            throw new NotImplementedException();
        }

        public Data.Models.Forum GetById(int id)
        {
            return _context.Forums
                 .Where(f => f.Id == id)        //Where is called on a primary key so will only return 1 result
                 .Include(f => f.Posts)         //When the Forum id is obtained, include the Post navigation property
                     .ThenInclude(p => p.User) //Then include the User navigation property on thos posts
                 .Include(f => f.Posts)
                     .ThenInclude(p => p.Replies)   //Then include replies made on that post
                         .ThenInclude(r => r.User)  //And also the user who made those replies
                  .FirstOrDefault();                //As there is only 1 result returned, first or default can be safely used
        }

        public Task UpdateForumDescription(int forumId, string newDescription)
        {
            throw new NotImplementedException();
        }

        public Task UpdateForumTitle(int forumId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
