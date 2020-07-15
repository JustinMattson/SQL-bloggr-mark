using System.ComponentModel.DataAnnotations;

namespace bloggr.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [MinLength(10)]
        public string Body { get; set; }
        public string Author { get; set; }
    }

    public class ViewModelBlogFavorite : Blog
    {
        public int FavoriteId { get; set; }
    }
}