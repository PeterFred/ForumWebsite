using Forum.Web.Models.Post;
using System.Collections.Generic;

namespace Forum.Web.Models
{
    public class ForumTopicModel
    {

        public ForumListingModel Forum { get; set; }
        public IEnumerable<PostListingModel> Posts { get; set; }
    }
}
