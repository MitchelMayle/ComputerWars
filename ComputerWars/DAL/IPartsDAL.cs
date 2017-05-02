using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWars.DAL
{
    public interface IPartsDAL
    {
        Dictionary<string, int> RandomizePrices();
    }
}