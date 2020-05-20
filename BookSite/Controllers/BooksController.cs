using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookSite.Data;
using BookSite.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;
using SkiaSharp;
using System.Drawing;

namespace BookSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        [HttpGet("mine/{authorId?}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetMyBooks(int authorId = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            if (authorId > 0)
            {
                // get series for a specific Author
                return await _context.Books
                    .Where(b => b.Authors.Any(a => a.AuthorId == authorId))
                    .ToListAsync();
            }
            else
            {
                var books = await _context.Books
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Where(b => b.Authors.Any(a => a.Author.SiteUserId == user.Id))
                    .ToListAsync();
                return books;
            }
        }

        [HttpGet("/api/series/mine/{authorId?}")]
        public async Task<ActionResult<IEnumerable<string>>> GetMySeries(int authorId = 0)
        {
            var user = await _userManager.GetUserAsync(User);
            if (authorId > 0)
            {
                // get series for a specific Author
                return await _context.Books
                    .Where(b => b.Authors.Any(a => a.AuthorId == authorId) && !String.IsNullOrEmpty(b.Series))
                    .Select(b => b.Series)
                    .ToListAsync();
            }
            else
            {
                var series = await _context.Books
                    .Include(b => b.Authors)
                    .ThenInclude(a => a.Author)
                    .Where(b => b.Authors.Any(a => a.Author.SiteUserId == user.Id) && !String.IsNullOrEmpty(b.Series))
                    .Select(b => b.Series)
                    .ToListAsync();
                return series;
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Covers)
                .Include(b => b.Sections)
                .Include(b => b.Blurbs)
                .Include(b => b.Authors).ThenInclude(a => a.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(BookSaveModel book)
        {
            var newBook = new Book{
                Title = book.Title,
                Subtitle = book.Subtitle,
                Series = book.Series,
                SeriesOrder = book.SeriesOrder,
                ComingSoon = true
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();


            foreach(var authorId in book.AuthorIds)
            {
                var newBookAuthor = new BookAuthor
                {
                    AuthorId = authorId,
                    BookId = newBook.Id
                };
                _context.BookAuthors.Add(newBookAuthor);   
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = newBook.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        [HttpPost("section/{id}/{sectionId?}")]
        public async Task<IActionResult> AddUpdateSection([FromRoute] int id, [FromForm] IFormFile backgroundImage,
            [FromForm] string name, [FromForm] string content, [FromForm] string backgroundColor, [FromForm] string color,
            [FromForm] bool deleteImage, [FromRoute] int? sectionId = null)
        {
            Section section;
            byte[] bytes = null;
            if (backgroundImage != null)
            {
                using (var stream = new MemoryStream())
                {
                    backgroundImage.CopyTo(stream);
                    stream.Position = 0;
                    using SKBitmap source = SKBitmap.Decode(stream);
                    int width = Convert.ToInt32(Convert.ToDouble(source.Width) * (1080 / Convert.ToDouble(source.Height)));
                    using SKBitmap resized = source.Resize(new SKImageInfo(width, 1080), SKFilterQuality.High);
                    bytes = resized.Encode(SKEncodedImageFormat.Png, 80).ToArray();
                }
            }

            if (sectionId == null)
            {
                // new section
                section = new Section
                {
                    Name = name,
                    Content = content,
                    BackgroundColor = backgroundColor,
                    Color = color,
                    BookId = id,
                    BackgroundImage = new CoverImage { Bytes = bytes }
                };
                _context.Sections.Add(section);
            } else
            {
                section = _context.Sections.Include(s => s.BackgroundImage).FirstOrDefault(s => s.Id == sectionId);
                if (section == null)
                {
                    return NotFound();
                }
                section.Name = name;
                section.Content = content;
                section.BackgroundColor = backgroundColor;
                section.Color = color;
                if (backgroundImage != null)
                {
                    section.BackgroundImage.Bytes = bytes;
                }
                if (deleteImage)
                {
                    _context.Images.Remove(section.BackgroundImage);
                }
            }
            _context.SaveChanges();
            return Ok(section);
        }

        [HttpPut("sectionorder/{id}")]
        public async Task<IActionResult> UpdateSectionOrder([FromBody] List<SectionOrderModel> sections, [FromRoute] int id)
        {
            var book = await _context.Books.Include(b => b.Sections).FirstOrDefaultAsync(b => b.Id == id);
            sections.OrderBy(s => s.Order).ToList().ForEach(s => { 
                if (s.Name == "Blurbs")
                {
                    book.BlurbOrder = s.Order;
                } else if (s.Name == "Buy")
                {
                    book.BuyOrder = s.Order;
                } else
                {
                    var section = book.Sections.FirstOrDefault(sec => sec.Id == s.Id);
                    if (section != null)
                    {
                        section.Order = s.Order;
                    }
                }
            });
            _context.SaveChanges();
            return Ok();
        }

        [HttpPost("coverart/{id}")]
        public async Task<IActionResult> UploadCoverArt([FromRoute] int id, [FromForm] IFormFile file, [FromForm] int type)
        {
            var book = await  _context.Books.Include(b => b.Covers).ThenInclude(c => c.Image).FirstOrDefaultAsync(b => b.Id == id);
            var coverArt = book.Covers.FirstOrDefault(c => c.Type == (CoverType)type);
            
            byte[] bytes;
            using (var stream = new MemoryStream()) {
                file.CopyTo(stream);
                stream.Position = 0;
                using SKBitmap source = SKBitmap.Decode(stream);
                int width = Convert.ToInt32(Convert.ToDouble(source.Width) * (900 / Convert.ToDouble(source.Height)));
                using SKBitmap resized = source.Resize(new SKImageInfo(width, 900),SKFilterQuality.High);
                                
                var image = new CoverImage { Bytes = resized.Encode(SKEncodedImageFormat.Png, 100).ToArray() };

                if (coverArt == null) {
                    coverArt = new CoverArt
                    {
                        BookId = book.Id,
                        Image = image,
                        ImageType = "image/png",
                        Height = 900,
                        Width = width,
                        Type = (CoverType)type,
                        IsDefault = book.Covers.Count == 0
                    };
                    book.Covers.Add(coverArt);
                } else
                {
                    var coverImage = coverArt.Image;
                    coverImage.Bytes = image.Bytes;

                    coverArt.Width = width;
                    coverArt.ImageType = file.ContentType;
                }
            }
            
            await _context.SaveChangesAsync();
            
            return Ok(coverArt);
        }

        [HttpPut("coverartsizings/{id}/{spine}/{flap}")]
        public async Task<IActionResult> UpdateCoverArtSizings(int id, float spine, float flap)
        {
            var coverArt = await _context.Covers.FindAsync(id);
            coverArt.SpineWidth = spine;
            coverArt.InsideFlapWidth = flap;
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("coverart/{id}/{type?}")]
        public async Task<IActionResult> CoverArt(int id, string type = null)
        {
            var book = await _context.Books.Include(b => b.Covers).ThenInclude(c => c.Image).Include(b => b.Authors).ThenInclude(a => a.Author).FirstOrDefaultAsync(b => b.Id == id);
            if (book.Covers.Count == 0)
            {
                return File(GenerateDefaultCover(book.Title, book.Subtitle, book.Series, book.Authors), "image/png");
            }
            if (type == null)
            {
                var defaultCover = book.Covers.SingleOrDefault(c => c.Type == CoverType.Front);
                if (defaultCover == null)
                {
                    defaultCover = book.Covers.SingleOrDefault(c => c.IsDefault);
                }
                return File(defaultCover.Image.Bytes, defaultCover.ImageType);
            }
            var cover = book.Covers.FirstOrDefault(c => c.Type == (CoverType)Convert.ToInt32(type));
            return File(cover.Image.Bytes, cover.ImageType);
        }

        [HttpGet("sectionbackground/{id}")]
        public async Task<IActionResult> SectionBackground(int id)
        {
            var section = await _context.Sections.Include(s => s.BackgroundImage).FirstOrDefaultAsync(s => s.Id == id);
            if (section == null || section.BackgroundImage == null)
            {
                return NotFound();
            }
            return File(section.BackgroundImage.Bytes, "image/png");
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }

        
        private byte[] GenerateDefaultCover(string title, string subtitle, string series, List<BookAuthor> authors)
        {
            SKImageInfo imageinfo = new SKImageInfo(600, 900);
            var imageBytes = new byte[] { };
            using (SKSurface surface = SKSurface.Create(imageinfo))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.DrawColor(SKColors.LightGray);

                // Title Front
                SKPaint textPaint100Black = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 100,

                };
                SKPaint textPaint50Black = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 50,
                };

                float titleWidth = textPaint100Black.MeasureText(title);
                canvas.DrawText(title, (600 - titleWidth) / 2, 100, textPaint100Black);

                // Authors Front

                authors.Reverse();
                var authorCount = 0;
                authors.ForEach(bookAuthor => {
                    var penName = bookAuthor.Author.PenName;
                    var authorWidth = textPaint50Black.MeasureText(penName);
                    canvas.DrawText(penName, (600 - authorWidth) / 2 , 900 - 50 - 50 * authorCount, textPaint50Black);
                    authorCount++;
                });
                var writtenByWidth = textPaint50Black.MeasureText("written by");
                canvas.DrawText("written by", (600 - writtenByWidth) / 2, 900 - 50 - 50 * authorCount, textPaint50Black);

                using (SKImage image = surface.Snapshot())
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (MemoryStream mStream = new MemoryStream(data.ToArray()))
                {
                    data.SaveTo(mStream);
                    return mStream.ToArray();
                }
            }

            return null;
        }
        private byte[] GenerateGenericCover(string title, string subtitle, string series, List<BookAuthor> authors)
        {
            SKImageInfo imageinfo = new SKImageInfo(1280, 900);
            var imageBytes = new byte[] { };
            using(SKSurface surface = SKSurface.Create(imageinfo))
            {
                SKCanvas canvas = surface.Canvas;
                canvas.DrawColor(SKColors.LightGray);

                // Title Front
                SKPaint textPaint100Black = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 100,
                    
                };
                SKPaint textPaint50Black = new SKPaint
                {
                    Color = SKColors.Black,
                    TextSize = 50,
                };

                float titleWidth = textPaint100Black.MeasureText(title);
                canvas.DrawText(title, (600 - titleWidth) / 2 + 680, 100, textPaint100Black);

                // Authors Front

                authors.Reverse();
                var authorCount = 0;
                authors.ForEach(bookAuthor => {
                    var penName = bookAuthor.Author.PenName;
                    var authorWidth = textPaint50Black.MeasureText(penName);
                    canvas.DrawText(penName, (600 - authorWidth) / 2 + 680, 900 - 50 - 50 * authorCount, textPaint50Black);
                    authorCount++;
                });
                var writtenByWidth = textPaint50Black.MeasureText("written by");
                canvas.DrawText("written by", (600 - writtenByWidth) / 2 + 680, 900 - 50 - 50 * authorCount, textPaint50Black);

                // Title Spine
                canvas.Save();
                canvas.RotateDegrees(90, 0, 0);
                canvas.DrawText(title, 80, -615, textPaint50Black);
                canvas.Restore();





                using (SKImage image = surface.Snapshot())
                using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (MemoryStream mStream = new MemoryStream(data.ToArray()))
                {
                    data.SaveTo(mStream);
                    return mStream.ToArray();
                }
            }

            return null;
        }
    }
}
