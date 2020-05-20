using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        [JsonIgnore]
        public string NavId
        {
            get { return Regex.Replace(Name.Trim().ToLower(), "[^a-z0-9 -]", ""); }
        }
        public string Content { get; set; }
        public string BackgroundColor { get; set; }
        public string Color { get; set; }
        [JsonIgnore]
        public int BackgroundImageId { get; set; }
        public CoverImage BackgroundImage { get; set; }
        public int Order { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
    }

    public class SectionOrderModel
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Name { get; set; }
    }
}
