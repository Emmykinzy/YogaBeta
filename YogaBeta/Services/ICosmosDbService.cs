using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YogaBeta.Model;

namespace YogaBeta.Services
{
    public interface ICosmosDbService
    {
        Task<List<Chakra>> GetChakrasAsync();
    }
}
