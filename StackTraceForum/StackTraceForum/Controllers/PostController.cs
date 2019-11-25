using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data;
using Forum.Data.Interfaces;
using Forum.Data.Models;
using Forum.Web.Models.Post;
using Forum.Web.Models.Reply;
using Microsoft.AspNetCore.Identity;
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
        private readonly IForum _forumService;

        //User manager records the session data for the current user. An asp.NET standard type
        //Provides the API's for interacting with the users in our data store
        private static UserManager<ApplicationUser> _userManager;

        public PostController(IPost postService, IForum forum, UserManager<ApplicationUser> userManager)
        {
            _postService = postService;
            _forumService = forum;
            _userManager = userManager;
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

        //Note id is Forum.id
        public IActionResult Create(int id)
        {
            var forum = _forumService.GetById(id);
            NewPostModel model = new NewPostModel
            {
                ForumName = forum.Title,
                ForumId = forum.Id,
                ForumImageUrl = forum.ImageUrl,
                AuthorName = User.Identity.Name    //User is system defined type
            };

            return View(model);
        }

        //Forms with post method will trigger here
        //Taking info from the user in the form of a viewmodel, 
        //then transform into an entity model that EntityFramework can push into the db
        [HttpPost]
        public async Task<IActionResult> AddPost(NewPostModel model)
        {
            string userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId); //Async task requires await

            var post = BuildPost(model, user);

            await _postService.Add(post);
            return RedirectToAction("Index", "Post", post.Id);
        }

        //Takes user entered data and converts to db acceptable data
        //Build backwards from a viewModel to an entity model
        public Post BuildPost(NewPostModel post, ApplicationUser user)
        {
            var now = DateTime.Now;
            var forum = _forumService.GetById(post.ForumId);

            return new Post
            {
                Title = post.Title,
                Content = post.Content,
                Created = now,
                Forum = forum,
                User = user
            };
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