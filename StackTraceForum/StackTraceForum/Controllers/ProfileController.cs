using Forum.Data.Interfaces;
using Forum.Data.Models;
using Forum.Web.Models.ApplicationUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Forum.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationUser _userService;
        private readonly IUpload _uploadService;
        private readonly IConfiguration _configuration;

        public ProfileController(UserManager<ApplicationUser> userManager,
                                IApplicationUser userService,
                                IUpload uploadService,
                                IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _uploadService = uploadService;
            _configuration = configuration;
        }


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

        [HttpPost]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = _userManager.GetUserId(User);

            //Connect to an Azure Storage Account Container (set in storageSettings.json)
            var connectionString = _configuration.GetConnectionString("AzureStorageAccount");
            
            //Get Blob container
            var container = _uploadService.GetBlobContainer(connectionString);

            // Parse Content Disposition Response Header on framework request 
            //(from the file that gets passed to the method from the upload forum form)
            var contentDisposition = ContentDispositionHeaderValue.Parse(file.ContentDisposition);

            //Grab the filename
            var filename = contentDisposition.FileName.Trim('"');

            //Get a reference to a Block blob
            var blockBlob = container.GetBlockBlobReference(filename);

            //On the block blob, upload our file <- file uploaded to the cloud
            await blockBlob.UploadFromStreamAsync(file.OpenReadStream());

            //Set the User's profile image to the URI
            await _userService.SetProfileImage(userId, blockBlob.Uri);

            //Redirect tot he Users profile page
            return RedirectToAction("Detail", "Profile", new { id = userId });
        }
    }
}