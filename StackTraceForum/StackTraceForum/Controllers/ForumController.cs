using Microsoft.AspNetCore.Mvc;
using StackTraceForums.Data;
using System.Collections.Generic;

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
            IEnumerable<Forum> forums = _forumService.GetAll();



            return View();
        }
    }
}