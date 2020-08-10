using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YogaBeta.Model;
using YogaBeta.Services;

namespace YogaBeta.Pages
{
    public class ClassPoseSelectModel : PageModel
    {
        private readonly ICosmosDbService cosmosDbService;
        [BindProperty]
        public List<Poses> PoseList { get; set; }
        [TempData]
        public List<Chakra> ChakraList { get; set; }

        public Poses newPose { get; set; }

        public ClassPoseSelectModel(
             ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }
        public async Task OnGetAsync()
        {
            ChakraList = await cosmosDbService.GetChakrasAsync();
            this.PoseList = new List<Poses>();
            int selectedPose;
            Random rnd = new Random();

            //Cycle thru each of the Chakras and randomly select a pose the current Chakra
            foreach (Chakra ch in ChakraList)
            {
                //Generate a random number between 1 and the number of poses
                selectedPose = rnd.Next(1, ch.Poses.Length);
                //Add the pose to the list
                PoseList.Add(ch.Poses[selectedPose]);
            }
        }

        public ActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
              
                string poseStr1 = HttpContext.Request.Form["NewPose-1"];
                string[] poseInfo = poseStr1.Split("/");
                poseStr1 = poseInfo[2];

                string poseStr2 = HttpContext.Request.Form["NewPose-2"];
                poseInfo = poseStr2.Split("/");
                poseStr2 = poseInfo[2];

                string poseStr3 = HttpContext.Request.Form["NewPose-3"];
                poseInfo = poseStr3.Split("/");
                poseStr3 = poseInfo[2];

                string poseStr4 = HttpContext.Request.Form["NewPose-4"];
                poseInfo = poseStr4.Split("/");
                poseStr4 = poseInfo[2];

                string poseStr5 = HttpContext.Request.Form["NewPose-5"];
                poseInfo = poseStr5.Split("/");
                poseStr5 = poseInfo[2];

                string poseStr6 = HttpContext.Request.Form["NewPose-6"];
                poseInfo = poseStr6.Split("/");
                poseStr6 = poseInfo[2];

                string poseStr7 = HttpContext.Request.Form["NewPose-7"];
                poseInfo = poseStr7.Split("/");
                poseStr7 = poseInfo[2];

                this.PoseList = new List<Poses>();

                PoseList.Add(ChakraList[0].Poses.Where(x => x.name == poseStr1).FirstOrDefault());
                PoseList.Add(ChakraList[1].Poses.Where(x => x.name == poseStr2).FirstOrDefault());
                PoseList.Add(ChakraList[2].Poses.Where(x => x.name == poseStr3).FirstOrDefault());
                PoseList.Add(ChakraList[3].Poses.Where(x => x.name == poseStr4).FirstOrDefault());
                PoseList.Add(ChakraList[4].Poses.Where(x => x.name == poseStr5).FirstOrDefault());
                PoseList.Add(ChakraList[5].Poses.Where(x => x.name == poseStr6).FirstOrDefault());
                PoseList.Add(ChakraList[6].Poses.Where(x => x.name == poseStr7).FirstOrDefault());
                    
  
                Poses[] TempArray = PoseList.ToArray();
                TempData.Set("Poses", TempArray);
                return RedirectToPage("ClassPreference");
            }
            return Page();
        }



    }
}
