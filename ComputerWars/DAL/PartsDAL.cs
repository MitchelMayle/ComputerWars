using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWars.DAL
{
    public class PricesDAL : IPricesDAL
    {
        public Dictionary<string, int> RandomizePrices()
        {
            Random changePrice = new Random();
            int cpuPrice = changePrice.Next(1500, 4001);
            int gpuPrice = changePrice.Next(601, 1601);
            int hddPrice = changePrice.Next(201, 701);
            int ramPrice = changePrice.Next(80, 301);
            int fshPrice = changePrice.Next(10, 101);

            Dictionary<string, int> partsList = new Dictionary<string, int>()
            {
                {"Processors", cpuPrice},
                {"Graphics Cards", gpuPrice},
                {"Hard Drives",hddPrice},
                {"RAM Sticks",ramPrice},
                {"Flash Drives", fshPrice}
            };

            return partsList;
        }
    }
}