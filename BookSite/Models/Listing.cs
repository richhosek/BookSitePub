﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookSite.Models
{
    public class Listing
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Format Format { get; set; }
        public int FormatId { get; set; }
        public Vendor Vendor { get; set; }
        public int VendorId { get; set; }
        public string Url { get; set; }
        public decimal? Price { get; set; }
    }
}
