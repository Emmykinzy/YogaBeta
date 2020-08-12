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
        public List<Poses> ClassPosesGenerated { get; set; }
 
        public List<List<Poses>> PosesByChakra { get; set; }
        [TempData]
        public int MyPose { get; set; }
        public ListChakras4Model(
            ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }
        public async Task OnGetAsync()
        {
            List<Chakra> chakras = await cosmosDbService.GetChakrasAsync();
            this.ClassPosesGenerated = new List<Poses>();
            this.PosesByChakra = new List<List<Poses>>();
            int selectedPose;
            Random rnd = new Random();

            //Cycle thru each of the Chakras and randomly select a pose the current Chakra
            foreach (Chakra ch in chakras)
            {
                //Generate a random number between 1 and the number of poses
                selectedPose = rnd.Next(1, ch.Poses.Length);
                //Add the pose to the list
                ClassPosesGenerated.Add(ch.Poses[selectedPose]);
                //Master list of all poses by Chakra
                this.PosesByChakra.Add(ch.Poses.ToList());
            }
        }
        public JsonResult OnGetPoseImage(int chakraIndex, String poseName)
        {

            Poses pose = (Poses)PosesByChakra[chakraIndex].Where(x => x.name == poseName);
            return new JsonResult(pose.picture);
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