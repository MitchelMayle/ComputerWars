using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWars.Models;

namespace ComputerWars.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Game()
        {
            if (Session["player"] == null)
            {
                Player player = new Player();
                Session["player"] = player;

                return View("Game", player);
            }
            else
            {
                return View("Game", Session["player"] as Player);
            }
        }
    }
}