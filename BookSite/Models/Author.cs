using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Author
    {
        public int Id { get; set; }
        public IdentityUser SiteUser { get; set; }
        public string SiteUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PenName { get; set; }
        public List<Collaborator> Collaborators { get; set; }
    }
}
