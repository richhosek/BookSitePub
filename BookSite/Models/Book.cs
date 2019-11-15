using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<BookAuthor> Authors { get; set; }
        public List<Edition> Editions { get; set; }
    }
}
