using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public class CommunityPost
    {
        [Key]
        private int CommunityPostId { get; set; }

        [Key]
        private int CommunityId { get; set; }

        [Required(ErrorMessage = "Titlul postarii este obligatoriu")]
        [StringLength(256, ErrorMessage = "Numele nu poate avea mai mult de 20 caractere")]
        private String Title { get; set; }

        private String PostType { get; set; }
        private String ImagePath { get; set; }
        private String Description { get; set; }
    }
}