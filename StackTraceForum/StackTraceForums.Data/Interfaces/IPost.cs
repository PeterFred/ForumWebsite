using Forum.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

//Utilise CRUD actions to interact with the DB
namespace Forum.Data.Interfaces
{
    public interface IPost
    {
        Post GetById(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetFilteredPosts(Data.Models.Forum forum, string seacrhQuery);
        IEnumerable<Post> GetFilteredPosts(string seacrhQuery);
        IEnumerable<Post> GetPostsbyForum(int id);
        IEnumerable<Post> GetLatestPosts(int n);

        Task Add(Post post);
        Task Delete(int id);
        Task EditPostContent(int id, string newContent);

        Task AddReply(PostReply reply);
    }
}
