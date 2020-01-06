using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Titlul postarii este obligatoriu")]
        [StringLength(256, ErrorMessage = "Numele nu poate avea mai mult de 20 caractere")]
        public String Title { get; set; }

        [Required(ErrorMessage = "Continutul nu poate fi gol postarii este obligatoriu")]
        public String Content { get; set; }

        public int CommunityId { get; set; }
        public Community Community { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}