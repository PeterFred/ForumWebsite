using Microsoft.AspNetCore.Mvc;
using StackTraceForum.Models;
using StackTraceForum.Models.Forum;
using StackTraceForums.Data;
using System.Collections.Generic;
using System.Linq;

namespace StackTraceForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }


        //Forum object is EntityModel - maps c# object back to 
        //the represtatio of this data in the data store.
        //Don't pass the raw entity model to the view, pass a viewModel instead.
        //Index page doen't need a full form object, create a simplified view model to pass down

        public IActionResult Index()
        {
            //Forums retrieves all objects from the database using GetAll()
            //Select uses Linq to map the properties on each Forum object into 
            //instances of the new forum listing model
            IEnumerable<ForumListingModel> forums = _forumService.GetAll()
                .Select(forum => new ForumListingModel
                {
                    Id = forum.Id,
                    Name = forum.Title,
                    Description = forum.Description
                });

            //Creates a collection of Forums
            var model = new ForumIndexModel
            {
                ForumList = forums
        };

            return View(model);
        }
    }
}