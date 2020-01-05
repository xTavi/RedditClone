
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public class Community
    {   [Key]
        public int CommunityId { get; set; }

        [Required(ErrorMessage = "Titlul este obligatoriu")]
        [StringLength(20, ErrorMessage = "Numele nu poate avea mai mult de 20 caractere")]

        public string Name { get; set; }
        public string Description { get; set; }


        [DataType(DataType.DateTime, ErrorMessage = "Campul trebuie sa contina data si ora")]
        public DateTime CreationTime { get; set; }

        //public int UserId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}