using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComputerWars.Models
{
    public class BuySellViewModel
    {
        public Player Player { get; set; }
        public string PartName { get; set; }

        [Required(ErrorMessage = "Please enter a qunatity.")]
        public int Quantity { get; set; }

        public static List<SelectListItem> Items { get; } = new List<SelectListItem>()
        {
            new SelectListItem() {Text="Processor", Value="Processors" },
            new SelectListItem() {Text="Graphics Card", Value="Graphics Cards" },
            new SelectListItem() {Text="Hard Drive", Value="Hard Drives" },
            new SelectListItem() {Text="RAM Stick", Value="RAM Sticks" },
            new SelectListItem() {Text="Flash Drive", Value="Flash Drives" },
        };
    }
}