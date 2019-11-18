using Microsoft.AspNetCore.Mvc;
using StackTraceForums.Data;

namespace StackTraceForum.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForum _forumService;

        public ForumController(IForum forumService)
        {
            _forumService = forumService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}