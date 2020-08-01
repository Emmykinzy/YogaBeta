using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using YogaBeta.Model;

namespace YogaBeta.Pages
{
    public class ClassPreferenceModel : PageModel
    {
        [BindProperty, Required, Display(Name = "Pose Duration (minutes)"), Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int PoseDuration { get; set; }
        [BindProperty, Required, Display(Name = "Duration(seconds) between poses"), Range(15, 60, ErrorMessage = "Value must be between {1} and {2}.")] 
        public int PrepDuration { get; set; }

        [TempData]
        public int MyPose { get; set; }

        public void OnGet()
        {
            this.PoseDuration = MyPose;
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                return RedirectToPage("OrderSuccess");
            }
            return Page();
        }
    }
}