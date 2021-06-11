using System;
using System.Collections.Generic;

// ADDED
using System.ComponentModel.DataAnnotations;
//

#nullable disable

namespace API.Models
{
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string content { get; set; }
        
        [Required]
        public int postId { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime createdAt { get; set; } = DateTime.Now;

        public virtual Post post { get; set; }

    }
}
