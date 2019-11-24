using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data.Interfaces;
using Forum.Data.Models;
using Forum.Web.Models.Post;
using Forum.Web.Models.Reply;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers
{
    //Contollers implement services, which are injected
    //Services are programmed against an interface so that we can
    // chose specific types of dependencies we want to inject
    //or the concrete classes that implement the actions that we define on each of our interfaces
    //Create ViewModels that our controller pushes that data from the service layer into in order to display to the user 
    public class PostController : Controller
    {
        //Make sure service is registered in startup.cs
        // services.AddScoped<IPost, postService>();
        private readonly IPost _postService;
        public PostController(IPost postService)
        {
            _postService = postService;
        }

        public IActionResult Index(int id)
        {
            Post post = _postService.GetById(id);

            IEnumerable<PostReplyModel> replies = BuildPostReplies(post.Replies);

            PostReplyModel model = new PostReplyModel
            {
                Id = post.Id,
                AuthorId = post.User.Id,
                AuthorName = post.User.UserName,
                AuthorImageUrl = post.User.ProfileImageUrl,
                AuthorRating = post.User.Rating,
                Created = post.Created,
                ReplyContent = post.Content,
            };

            return View(model);
        }

        private IEnumerable<PostReplyModel> BuildPostReplies(IEnumerable<PostReply> replies)
        {
            return replies.Select(reply => new PostReplyModel
            {
                Id = reply.Id,
                AuthorName = reply.User.UserName,
                AuthorId = reply.User.Id,
                AuthorImageUrl = reply.User.ProfileImageUrl,
                AuthorRating = reply.User.Rating,
                Created = reply.Created,
                ReplyContent = reply.Content
            });
        }
    }
}