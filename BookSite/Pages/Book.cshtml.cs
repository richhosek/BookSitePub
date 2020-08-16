using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BookSite.Data;
using BookSite.Models;

namespace BookSite
{
    public class BookModel : PageModel
    {
        private readonly BookSite.Data.ApplicationDbContext _context;

        public BookModel(BookSite.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Book Book { get; set; }
        public Author Author { get; set; }

        public async Task<IActionResult> OnGetAsync(string penNameUrl, string titleUrl = null)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            Author = await _context.Authors.FirstOrDefaultAsync(a => a.PenNameUrl == penNameUrl);
            if (Author == null)
            {
                return NotFound();
            }
            Book = await _context.Books
                .Include(b => b.Covers)
                .Include(b => b.Sections)
                .Include(b => b.Blurbs)
                .Include(b => b.Authors)
                .ThenInclude(a => a.Author)
                .ThenInclude(a => a.Books)
                .ThenInclude(b => b.Book)
                .Include(b => b.Editions)
                .ThenInclude(e => e.Formats)
                .ThenInclude(f => f.Listings)
                .ThenInclude(l => l.Vendor)
                .FirstOrDefaultAsync(b => b.TitleUrl == titleUrl && b.Authors.Any(a => a.AuthorId == Author.Id));

            if (Book == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
