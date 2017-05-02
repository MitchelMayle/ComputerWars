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

        [Required(ErrorMessage = "Enter a name.")]
        [MinLength(2, ErrorMessage = "Name must contain at least 2 letters.")]
        [MaxLength(20, ErrorMessage = "Name can not be more than 20 characters.")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Name can only contain letters.")]
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

        public bool HasParts()
        {
            int total = 0;

           foreach (KeyValuePair<string, int> kvp in Inventory)
            {
                total += kvp.Value;
            }

            return total > 0;
        }
    }
}