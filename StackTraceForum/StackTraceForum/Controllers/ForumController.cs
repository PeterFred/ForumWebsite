using Microsoft.AspNetCore.Mvc;
using Forum.Data;
using Forum.Web.Models.Forum;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Web.Controllers
{
    //Methods are defined in Forums.Data.IForum
    //COncrete classes are implmented in Forums.Service.ForumService
    public class ForumController : Controller
    {
        //Make sure service is registered in startup.cs
        // services.AddScoped<IForum, ForumService>();
        private readonly IForum _forumService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
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
    
        //Return a forum identified by its primary key

        public IActionResult Topic(int id)
        {
            var forum = _forumService.GetById(id);



            return View();
        }
    }


}