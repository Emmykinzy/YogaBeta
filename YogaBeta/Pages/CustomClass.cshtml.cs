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
    public class CustomClassModel : PageModel
    {
        private readonly ICosmosDbService cosmosDbService;
        public List<Poses> Crown { get; set; }
        public List<Poses> ThirdEye { get; set; }
        public List<Poses> Throat { get; set; }
        public List<Poses> Heart { get; set; }
        public List<Poses> Solar { get; set; }
        public List<Poses> Sacral { get; set; }
        public List<Poses> Root { get; set; }
        public List<Chakra> ChakraList { get; set; }
        public CustomClassModel(
             ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }
        public async Task OnGetAsync()
        {
            ChakraList = await cosmosDbService.GetChakrasAsync();
 
            this.Crown = ChakraList[0].Poses.ToList();
            this.ThirdEye = ChakraList[1].Poses.ToList();
            this.Throat = ChakraList[2].Poses.ToList();
            this.Heart = ChakraList[3].Poses.ToList();
            this.Solar = ChakraList[4].Poses.ToList();
            this.Sacral = ChakraList[5].Poses.ToList();
            this.Root = ChakraList[6].Poses.ToList();

        }
    }
}
