using Microsoft.AspNetCore.Mvc;
using Forum.Data;
using Forum.Web.Models.Forum;
using System.Collections.Generic;
using System.Linq;
using Forum.Data.Interfaces;
using Forum.Web.Models.Post;
using Forum.Data.Models;
using System;
using Forum.Web.Models;

namespace Forum.Web.Controllers
{
    //Methods are defined in Forums.Data.IForum
    //COncrete classes are implmented in Forums.Service.ForumService
    public class ForumController : Controller
    {
        //Make sure service is registered in startup.cs
        // services.AddScoped<IForum, ForumService>();
        private readonly IForum _forumService;
        private readonly IPost _postService;

        public ForumController(IForum forumService, IPost postService)
        {
            _forumService = forumService;
            _postService = postService;

        }


        #region IndexController
        //Forum object is EntityModel - maps c# object back to 
        //the represtatio of this data in the data store.
        //Don't pass the raw entity model to the view, pass a viewModel instead.
        //Index page doen't need a full form object, create a simplified view model to pass down

        public IActionResult Index()
        {
            //Forums retrieves all objects from the database using GetAll()
            //Select uses Linq to map the properties on each Forum object into 
            //instances of the new forum listing model
            IEnumerable<ForumListingModel> forums = _forumService.GetAll() //From the service layer
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description
                });

            //Creates a collection of Forums
            var model = new ForumIndexModel
            {
                ForumList = forums //Populated from the above service layer
            };
            //The entrie wrapped forum index model is passed to the view,
            //and accessed in the Views/Forum/Index.cshtml by the first line in razor
            //MVC will look for a View called Index, in a folder called Forum
            return View(model);
        }
        #endregion

        #region TopicController
        //Return a forum and associated posts identified by its primary key
        public IActionResult Topic(int id, string searchQuery)
        {
            Data.Models.Forum forum = _forumService.GetById(id);
            IEnumerable<Post> posts = new List<Post>();


            //IEnumerable<Post> posts = _postService.GetPostsbyForum(id);
            //Refactored:
            //Get all the posts from the forum, then enumerate them to a list,
            //Then provide them to the viewModel
            //Nb nullOrEmpty strings checked in method

            posts = _postService.GetFilteredPosts(forum, searchQuery).ToList();


            var postListings = posts.Select(post => new PostListingModel
            {
                Id = post.Id,
                AuthorName = post.User.UserName,
                AuthorId = post.User.Id,
                AuthorRating = post.User.Rating,
                Title = post.Title,
                DatePosted = post.Created.ToString(),
                RepliesCount = post.Replies.Count(),
                Forum = BuildForumListing(post)
            });

            var model = new ForumTopicModel
            {
                Posts = postListings,
                Forum = BuildForumListing(forum)
            };
           
            return View(model);
        }

        #endregion


        /**
         * Return the same forum post back, but containing posts 
         * that correspond to the seacrh query of that forum.
         * Therefore, new model not required, but can utilise TopiCController
         * to return all the posts, or a filtered list.
         * SO, acts like a wrapper to redirect to the TopicController
         */
        [HttpPost]
        public IActionResult Search(int id, string searchQuery)
        {
            return RedirectToAction("Topic", new { id, searchQuery });
        }

        #region PrivateFunctions

        private ForumListingModel BuildForumListing(Post post)
        {
            Data.Models.Forum forum = post.Forum;
            return BuildForumListing(forum);
        }

        private ForumListingModel BuildForumListing(Data.Models.Forum forum)
        {
            return new ForumListingModel
            {
                Id = forum.Id,
                Name = forum.Title,
                Description = forum.Description,
                ImageUrl = forum.ImageUrl
            };
        }

        #endregion
    }


}