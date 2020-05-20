using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSite.Areas.Author.Pages
{
    public class newProfileModel : PageModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PenName { get; set; }
        public void OnGet()
        {
            var user = User;
        }
    }
}
