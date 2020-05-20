using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleUrl { get; set; }
        public string Subtitle { get; set; }
        public List<BookAuthor> Authors { get; set; }
        public List<Edition> Editions { get; set; }
        public string Series { get; set; }
        public int? SeriesOrder { get; set; }
        public bool IsActive { get; set; }
        public bool ComingSoon { get; set; }
        public DateTime? PublishedOn { get; set; }
        public List<CoverArt> Covers { get; set; }
        public List<Section> Sections { get; set; }
        public int BlurbOrder { get; set; }
        public int BuyOrder { get; set; }
        public List<Blurb> Blurbs { get; set; }
    }

    public class BookSaveModel
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Series { get; set; }
        public int? SeriesOrder { get; set; }
        public int[] AuthorIds { get; set; }

    }
}
