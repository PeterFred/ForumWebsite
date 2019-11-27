using Forum.Data;
using Forum.Data.Interfaces;
using Forum.Data.Models;
using Microsoft.EntityFrameworkCore;
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

        //Adds posts to the db
        public async Task Add(Post post)
        {
            _context.Add(post);
            await _context.SaveChangesAsync();
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
            return _context.Posts
                .Include(post => post.User)
                .Include(post => post.Replies).ThenInclude(reply => reply.User)
                .Include(post => post.Forum);
        }

        //Instantiated using lazy loading so needs to use include
        public Post GetById(int id)
        {
            return _context.Posts
                .Where(p => p.Id == id)
                .Include(post => post.User)
                .Include(post => post.Replies)
                    .ThenInclude(reply=>reply.User)
                .Include(post => post.Forum)
                .First();
        }

        /*Gets called from ForumController.
         * Get the forum for the particular topic (using Entyity Framework to find it by id),
         * then return matching posts (title or content) using where
         */
        public IEnumerable<Post> GetFilteredPosts(Data.Models.Forum forum, string searchQuery)
        {

            //Check if string is null, in which case return all posts, otherwise filtered posts
            return String.IsNullOrEmpty(searchQuery)
                ?forum.Posts
                : forum.Posts.Where(post => 
                        post.Title.Contains(searchQuery)
                        || post.Content.Contains(searchQuery));
        }

        /**
         * Acts on all posts in the system
         */
        public IEnumerable<Post> GetFilteredPosts(string searchQuery)
        {
            return GetAll().Where(post => 
                        post.Title.Contains(searchQuery)
                        || post.Content.Contains(searchQuery));
        }

        public IEnumerable<Post> GetLatestPosts(int n)
        {
           return  GetAll().OrderByDescending(post => post.Created).Take(n);
        }

        public IEnumerable<Post> GetPostsbyForum(int id)
        {
            return _context.Forums.Where(f => f.Id == id)
                .First().Posts;
        }
    }
}
