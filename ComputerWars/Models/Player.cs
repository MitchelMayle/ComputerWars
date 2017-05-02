using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComputerWars.Models
{
    public class Player
    {
        public Player()
        {
            Money = 1000;
            CurrentDay = 1;
        }

        [Required(ErrorMessage = "A name is required.")]
        public string Name { get; set; }

        public int CurrentDay { get; set; }
        public int Money { get; set; }

        public Dictionary<string, int> Prices = new Dictionary<string, int>();

        public Dictionary<string, int> Inventory = new Dictionary<string, int>()
        {
            {"Processors", 0 },
            {"Graphics Cards", 0 },
            {"Hard Drives", 0 },
            {"RAM Sticks", 0 },
            {"Flash Drives", 0 }
        };
    }
}