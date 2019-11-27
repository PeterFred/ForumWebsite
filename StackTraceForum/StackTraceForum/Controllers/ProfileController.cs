using Forum.Data.Interfaces;
using Forum.Data.Models;
using Forum.Web.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Forum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;

        public ProfileController(UserManager<ApplicationUser> userManager, IApplicationUser userService, IUpload uploadService)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
        }

        [Authorize]
        public IActionResult Detail(string id)
        {
            //Grab the user to map to the model
            ApplicationUser user = _userService.GetById(id);
            IList<string> userRoles = _userManager.GetRolesAsync(user).Result;

            var model = new ProfileModel()
            {
                UserId = user.Id,
                Username = user.UserName,
                UserRating = user.Rating.ToString(),
                Email = user.Email,
                ProfileImageUrl = user.ProfileImageUrl,
                IsAdmin = userRoles.Contains("Admin"),
                MemberSince = user.MemberSince
            };

            return View(model);
        }
    }
}