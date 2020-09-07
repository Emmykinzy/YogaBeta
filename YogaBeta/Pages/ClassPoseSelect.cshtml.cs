using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Nancy.Json;
using YogaBeta.Model;
using YogaBeta.Services;

namespace YogaBeta.Pages
{
    public class ClassPoseSelectModel : PageModel
    {
        private readonly ICosmosDbService cosmosDbService;
        [BindProperty]
        public string NewPose1 { get; set; }
        [BindProperty]
        public string NewPose2 { get; set; }
        [BindProperty]
        public string NewPose3 { get; set; }
        [BindProperty]
        public string NewPose4 { get; set; }
        [BindProperty]
        public string NewPose5 { get; set; }
        [BindProperty]
        public string NewPose6 { get; set; }
        [BindProperty]
        public string NewPose7 { get; set; }

        [BindProperty]
        public List<Poses> PoseList { get; set; }
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

        public async Task<ActionResult> OnPost()
        {

            if (ModelState.IsValid)
            {
                string[] newPoses = new string[7];
                string[] jsonPoses = new string[7];

                string poseStr1 = HttpContext.Request.Form["NewPose_1"];
                string[] poseInfo = poseStr1.Split("/");
                newPoses[0] = poseInfo[2];

                string poseStr2 = HttpContext.Request.Form["NewPose_2"];
                poseInfo = poseStr2.Split("/");
                newPoses[1] = poseInfo[2];

                string poseStr3 = HttpContext.Request.Form["NewPose_3"];
                poseInfo = poseStr3.Split("/");
                newPoses[2] = poseInfo[2];

                string poseStr4 = HttpContext.Request.Form["NewPose_4"];
                poseInfo = poseStr4.Split("/");
                newPoses[3] = poseInfo[2];

                string poseStr5 = HttpContext.Request.Form["NewPose_5"];
                poseInfo = poseStr5.Split("/");
                newPoses[4] = poseInfo[2];

                string poseStr6 = HttpContext.Request.Form["NewPose_6"];
                poseInfo = poseStr6.Split("/");
                newPoses[5] = poseInfo[2];

                string poseStr7 = HttpContext.Request.Form["NewPose_7"];
                poseInfo = poseStr7.Split("/");
                newPoses[6] = poseInfo[2];

                this.PoseList = new List<Poses>();
                ChakraList = await cosmosDbService.GetChakrasAsync();
                for (int i = 0; i < 7; i++)
                {
                    PoseList.Add(ChakraList[i].Poses.Where(x => x.name == newPoses[i]).FirstOrDefault());
                    jsonPoses[i] = new JavaScriptSerializer().Serialize(PoseList[i]);
                }


                Poses[] TempArray = PoseList.ToArray();
                //TempData.Set("Poses", TempArray);
                //TempData["jsonPoses"] = jsonPoses;
                TempData.Set("PoseList", PoseList.ToArray());
                return RedirectToPage("ClassPreference");
            }
            return Page();
        }



    }
}
