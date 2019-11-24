using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Forum.Data.Interfaces;
using Forum.Web.Models.Post;
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
            Data.Models.Post post = _postService.GetById(id);

            var model = new PostIndexModel
            {

            };

            return View();
        }
    }
}