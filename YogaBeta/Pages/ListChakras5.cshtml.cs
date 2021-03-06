﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using YogaBeta.Model;
using YogaBeta.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace YogaBeta.Pages
{
    public class ListChakras5Model : PageModel
    {
        [BindProperty]
        public List<Poses> PoseList { get; set; }
        public List<Chakra> ChakraList { get; set; }

        [BindProperty]
        public int FromStage { get; set; }

        private readonly ICosmosDbService cosmosDbService;
        public ListChakras5Model(ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }
        public async Task OnGetAsync()
        {
            ChakraList = await cosmosDbService.GetChakrasAsync();

            //Test Serializing data to Json
            //String jsonChakra = JsonSerializer.Serialize<List<Chakra>>(ChakraList);

            this.PoseList = new List<Poses>();
            int selectedPose;
            Random rnd = new Random();

            //Cycle thru each of the Chakras and randomly select a pose the current Chakra
            foreach (Chakra ch in ChakraList)
            {
                //Generate a random number between 1 and the number of poses
                selectedPose = rnd.Next(0, ch.Poses.Length -1);
                //Add the pose to the list
                PoseList.Add(ch.Poses[selectedPose]);
            }
        }

        public async Task<JsonResult> OnGetPoseImageAsync(int index, String poseName)
        {
            //Retrieve the entire chakra list
            List<Chakra> ChakraList = await cosmosDbService.GetChakrasAsync();

            //Using the index value and pose name retrieve the Pose.
            Poses p = ChakraList[index].Poses.Where(x => x.name == poseName).FirstOrDefault();

            return new JsonResult(p);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //If coming from the Class Settings Page
            if (FromStage == 2)
            {
                //Load the Poses already selected.
                this.PoseList = TempData.Get<Poses[]>("PoseList").ToList();

                //Load the Charkra list. This is for the Select Lists of poses
                ChakraList = await cosmosDbService.GetChakrasAsync();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    //Save the list of poses for later use.
                    TempData.Set("PoseList", PoseList.ToArray());

                    return RedirectToPage("ClassPreference2");
                }
            }
            return Page();
        }
    }
}