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

        [BindProperty, Display(Name = "Shavasana")]
        public string Shavasana { get; set; }
        public string[] ShavasanaOptions = new[] { "Before Class", "After Class", "None" }; 

        [BindProperty, Display(Name = "Duration(minutes)"), Range(0, 30, ErrorMessage = "Value for {0} may not exceed {2} minutes.")]
        public int ShavasanaLength { get; set; }

        public int MyPose { get; set; }
        public List<Poses> Poses { get; set; }

        public void OnGet()
        {
            this.PoseDuration = MyPose;
            //this.Poses = TempData.Get<Poses[]>("PoseList").ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                YogaClass yc = new YogaClass();

                if(Shavasana != "None")
                {
                    yc.ShavasanaLength = ShavasanaLength;              
                }  

                yc.PoseDuration = PoseDuration;
                yc.PrepDuration = PrepDuration;
                yc.Shavasana = Shavasana;
                yc.PoseList = TempData.Get<Poses[]>("PoseList");

                TempData["YogaClass"] = yc.ToString();
                return RedirectToPage("Confirmation");
            }
            return Page();
        }
    }
}