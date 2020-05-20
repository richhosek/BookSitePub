//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using BookSite.Data;
//using BookSite.Models;
//using Microsoft.AspNetCore.Identity;

//namespace BookSite.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SeriesController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly UserManager<IdentityUser> _userManager;

//        public SeriesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }

//        // GET: api/Series
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Series>>> GetSeries()
//        {
//            return await _context.Series.ToListAsync();
//        }

//        [HttpGet("mine/{authorId?}")]
//        public async Task<ActionResult<IEnumerable<Series>>> GetMySeries(int authorId = 0)
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (authorId > 0)
//            {
//                // get series for a specific Author
//                return await _context.Books
//                    .Where(b => b.Authors.Any(a => a.AuthorId == authorId) && b.SeriesId != null)
//                    .Select(b => b.Series).ToListAsync();
//            }else
//            {
//                return await _context.Books
//                    .Include(b => b.Authors)
//                    .ThenInclude(a => a.Author)
//                    .Where(b => b.Authors.Any(a => a.Author.SiteUserId == user.Id) && b.SeriesId != null)
//                    .Select(b => b.Series).ToListAsync();
//            }
//        }

//        // GET: api/Series/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Series>> GetSeries(int id)
//        {
//            var series = await _context.Series.FindAsync(id);

//            if (series == null)
//            {
//                return NotFound();
//            }

//            return series;
//        }

//        // PUT: api/Series/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutSeries(int id, Series series)
//        {
//            if (id != series.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(series).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!SeriesExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Series
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
//        // more details see https://aka.ms/RazorPagesCRUD.
//        [HttpPost]
//        public async Task<ActionResult<Series>> PostSeries(Series series)
//        {
//            _context.Series.Add(series);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetSeries", new { id = series.Id }, series);
//        }

//        // DELETE: api/Series/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<Series>> DeleteSeries(int id)
//        {
//            var series = await _context.Series.FindAsync(id);
//            if (series == null)
//            {
//                return NotFound();
//            }

//            _context.Series.Remove(series);
//            await _context.SaveChangesAsync();

//            return series;
//        }

//        private bool SeriesExists(int id)
//        {
//            return _context.Series.Any(e => e.Id == id);
//        }
//    }
//}
