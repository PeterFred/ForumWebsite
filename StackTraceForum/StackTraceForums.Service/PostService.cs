using Forum.Data;
using Forum.Data.Interfaces;
using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Service
{
    public class PostService : IPost
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task Add(Post post)
        {
            throw new NotImplementedException();
        }

        public Task AddReply(PostReply reply)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPostContent(int id, string newContent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetAll()
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Post> GetFilteredPosts(string seacrhQuery)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Post> GetPostsbyForum(int id)
        {
            return _context.Forums.Where(f => f.Id == id)
                .First().Posts;
        }
    }
}
