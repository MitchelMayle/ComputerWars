using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ComputerWars.Models
{
    public class BuySellViewModel
    {
        public Player Player { get; set; }
        public string PartName { get; set; }
        public int Quantity { get; set; } 
    }
}