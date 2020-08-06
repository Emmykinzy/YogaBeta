using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YogaBeta.Services;
using YogaBeta.Model;

namespace YogaBeta.Pages
{
    public class ListChakras4Model : PageModel
    {
        private readonly ICosmosDbService cosmosDbService;
        [BindProperty]
        public List<Poses> Poses { get; set; }
        [TempData]
        public int MyPose { get; set; }
        Poses[] TempArray { get; set; }
        public ListChakras4Model(
            ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }
        public async Task OnGetAsync()
        {
            List<Chakra> chakras = await cosmosDbService.GetChakrasAsync();
            this.Poses = new List<Poses>();
            int selectedPose;
            Random rnd = new Random();

            //Cycle thru each of the Chakras and randomly select a pose the current Chakra
            foreach (Chakra ch in chakras)
            {
                //Generate a random number between 1 and the number of poses
                selectedPose = rnd.Next(1, ch.Poses.Length);
                //Add the pose to the list
                Poses.Add(ch.Poses[selectedPose]);
            }
        }

        public ActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                MyPose = 5;
                //List<Poses> p = TempData["Poses"] as List<Poses>;
                //Poses[] TempArray = Poses.ToArray();
                //TempData.Set("Poses", TempArray);
                //this.Poses = TempData.Get<Poses[]>("Poses").ToList();
                return RedirectToPage("ClassPreference");
            }
            return Page();
        }

    }
}