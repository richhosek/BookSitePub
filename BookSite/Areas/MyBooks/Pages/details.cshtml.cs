using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSite.Areas.MyBooks.Pages
{
    public class detailsModel : PageModel
    {
        public int BookId { get; set; }
        public void OnGet(int id)
        {
            BookId = id;
        }
    }
}
