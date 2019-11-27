using Forum.Web.Models.Post;
using System.Collections.Generic;

namespace Forum.Web.Models.Search
{
    public class SearchResultModel
    {
        public IEnumerable<PostListingModel> Posts { get; set; }
        public string Searchquery { get; set; }
        public bool EmptySearchResults { get; set; }
    }
}
