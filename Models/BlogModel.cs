using System.ComponentModel.DataAnnotations;

namespace BlogMgtAPI.Models
{
    public class BlogModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
