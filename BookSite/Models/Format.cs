using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Format
    {
        public int Id { get; set; }
        public Edition Edition { get; set; }
        public int EditionId { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
