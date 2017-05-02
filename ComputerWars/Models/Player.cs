using System;
using System.Collections.Generic;
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

        public string Name { get; set; }
        public int CurrentDay { get; set; }
        public int Money { get; set; }

        private Dictionary<string, int> Inventory = new Dictionary<string, int>()
        {
            {"Processors", 0 },
            {"Grpahics Cards", 0 },
            {"Hard Drives", 0 },
            {"RAM Sticks", 0 },
            {"Flash Drives", 0 }
        };

        public string[] GetItemName()
        {
            List<string> itemNames = new List<string>();

            foreach (KeyValuePair<string, int> kvp in Inventory)
            {
                itemNames.Add(kvp.Key);
            }

            return itemNames.ToArray();
        }

        public int GetItemQuantity(string itemName)
        {
            return Inventory[itemName];
        }

        public void IncreaseQuantity(string partName, int increaseAmount)
        {
            Inventory[partName] += increaseAmount;
        }

        public void DecreaseQuantity(string partName, int decreaseAmount)
        {
            Inventory[partName] -= decreaseAmount;
        }
    }
}