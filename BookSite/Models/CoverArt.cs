using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class CoverArt
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }
        public int ImageId { get; set; }
        [JsonIgnore]
        public CoverImage Image { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float SpineWidth { get; set; }
        public float InsideFlapWidth { get; set; }
        public string ImageType { get; set; }
        public CoverType Type { get; set; }
        public bool IsDefault { get; set; }
    }

    public class CoverImage
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
    }

    public enum CoverType
    {
        Front = 1,
        Full = 2,
        Dustjacket = 3,
        Audiobook =4
    }
}
