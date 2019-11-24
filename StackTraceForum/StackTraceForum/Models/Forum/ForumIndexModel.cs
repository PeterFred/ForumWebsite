using System.Collections.Generic;

namespace Forum.Web.Models.Forum
{
    //This passes down to the view.
    //A wrapper class for returning a collection of ForumListingModels
    public class ForumIndexModel
    {
        public IEnumerable<ForumListingModel> ForumList { get; set; }
    }
}
