using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RedditClone.Models
{
    public class Community
    {
        public int CommunityId { get; set; }
        public string Name { get; set; }

    }

    public class CommunityDBContext : DbContext
    {
        public CommunityDBContext() : base("C:\USERS\CONST\DESKTOP\CACAT\REDDITCLONE\REDDITCLONE\APP_DATA\BAZASPATIALA.MDF") { }
        public DbSet<Community> Communities { get; set; } // Not sure if this is working properly or the line above 
    }
}