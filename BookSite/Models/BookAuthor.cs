using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class BookAuthor
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        public int BookId { get; set; }
        public Author Author { get; set; }
        public int AuthorId { get; set; }
        public int Order { get; set; }
        public bool IsManager { get; set; }
    }
}
