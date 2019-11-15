using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Edition
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Book Book { get; set; }
        public int BookId { get; set; }
        public DateTime PublishedOn { get; set; }
        public List<Format> Formats { get; set; }
    }

}
