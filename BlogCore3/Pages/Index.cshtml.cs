using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ReflectionIT.Mvc.Paging;

namespace BlogCore3.Pages
{
    public class BlogPostModel : PageModel
    {
        public void OnGet()
        {
            ViewData["heading"] = "Welcome to ASP.NET Core Razor Pages !!";


        }

        

    }
}