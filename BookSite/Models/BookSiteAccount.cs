using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class BookSiteAccount
    {
        public int Id { get; set; }
        public IdentityUser User { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // Author, Agent, Publisher
    }
}
