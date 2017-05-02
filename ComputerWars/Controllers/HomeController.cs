using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWars.Models;
using ComputerWars.DAL;

namespace ComputerWars.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartsDAL partsDAL;

        public HomeController(IPartsDAL partsDAL)
        {
            this.partsDAL = partsDAL;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Game()
        {
            if (Session["player"] == null)
            {
                Player player = new Player();
                player.Prices = partsDAL.RandomizePrices();

                Session["player"] = player;

                return View("Game", player);
            }
            else
            {
                return View("Game", Session["player"] as Player);
            }
        }

        public ActionResult Prices()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Prices", Session["player"] as Player);
        }

        public ActionResult Inventory()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Inventory", Session["player"] as Player);
        }

        public ActionResult Buy()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Buy", Session["player"] as Player);
        }

        public ActionResult Sell()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Sell", Session["player"] as Player);
        }

        public ActionResult Casino()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Casino", Session["player"] as Player);
        }

        public ActionResult Airport()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Airport", Session["player"] as Player);
        }
    }
}