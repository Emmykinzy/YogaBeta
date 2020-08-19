using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace YogaBeta.Pages
{
    public class ClassConfirmationModel : PageModel
    {
        [BindProperty]
        public int PoseDuration { get; set; }
        public void OnGet()
        {

        }

        public ActionResult OnPost()
        {
            return Page();
        }
    }
}