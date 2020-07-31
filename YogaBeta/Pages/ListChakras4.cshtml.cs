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
        public List<Chakra> chakras { get; }
        public Chakra c { get; set; }
        public Poses Poses { get; set; }

        public ListChakras4Model(
            ICosmosDbService cosmosDbService)
        {
            this.cosmosDbService = cosmosDbService;
        }
        public async Task OnGetAsync()
        {
            List<Chakra> chakras;

            chakras = await cosmosDbService.GetChakrasAsync();
            c = chakras[0];
        }
    }
}