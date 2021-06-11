using System;
using System.Collections.Generic;

// ADDED
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//



namespace API.Models
{
    public partial class Post
    {
        public Post()
        {
            comments = new HashSet<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string content { get; set; }

        [DataType(DataType.Date)]
        public DateTime? createdAt { get; set; } = DateTime.Now;
        
        public virtual ICollection<Comment> comments { get; set; }
    }
}
