using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Blurb
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Source { get; set; }
        public int Order { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
    }
}
