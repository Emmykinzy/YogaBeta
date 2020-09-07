using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using YogaBeta.Model;
using System.Text.Json;
using System.Runtime.InteropServices.ComTypes;

namespace YogaBeta.Pages
{
    public class ClassPreferenceModel2 : PageModel
    {
        [BindProperty, Required, Display(Name = "Pose Duration (minutes)"), Range(1, 5, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int PoseDuration { get; set; } = 4;

        [BindProperty, Required, Display(Name = "Duration between poses (seconds)"), Range(15, 60, ErrorMessage = "Value must be between {1} and {2}.")]
        public int PrepDuration { get; set; } = 15;

        [BindProperty, Required, Display(Name = "Shavasana")]
        public string Shavasana { get; set; } = "None";

        public string[] ShavasanaOptions = new[] { "Before Class", "After Class", "None" };

        [BindProperty, Display(Name = "Duration(minutes)"), Range(0, 30, ErrorMessage = "Value for {0} may not exceed {2} minutes.")]
        public int ShavasanaLength { get; set; } = 0;

        [BindProperty]
        public int FromStage { get; set; }

        public void OnGet()
        {

        }

        public ActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                var x = TempData["PoseList"] as String;
                Poses[] p = JsonSerializer.Deserialize<Poses[]>(x);

                YogaClass yc = new YogaClass
                {
                    PoseDuration = PoseDuration,
                    PrepDuration = PrepDuration,
                    Shavasana = Shavasana,
                    ShavasanaLength = ShavasanaLength,
                    PoseList = p
                };

                TempData["YogaClass"] = yc.ToString();

                //Redirecting to a Razor component that has been created as a page.
                //return Redirect("~/ClassConfirmation");
                return RedirectToPage("ClassConfirmation");
            }
            return Page();
        }
    }
}