using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Collaborator
    {
        public int Id { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public Author CoAuthor { get; set; }
        public int CoAuthorId { get; set; }
    }
}
