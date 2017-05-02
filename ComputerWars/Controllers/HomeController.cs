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
        private readonly IPricesDAL pricesDAL;

        public HomeController(IPricesDAL pricesDAL)
        {
            this.pricesDAL = pricesDAL;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public ActionResult NewGame()
        {
            if (Session["player"] != null)
            {
                return RedirectToAction("Menu");
            }

            return View("NewGame");
        }

        [HttpPost]
        public ActionResult NewGame(Player player)
        {
            if (!ModelState.IsValid)
            {
                return View("NewGame", player);
            }

            player.Prices = pricesDAL.RandomizePrices();
            Session["player"] = player;

            return RedirectToAction("Menu");
        }

        public ActionResult Menu()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            Player player = Session["player"] as Player;

            if (player.CurrentDay > 30)
            {
                return RedirectToAction("GameOver");
            }

            return View("Menu", player);
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

        [HttpGet]
        public ActionResult Buy()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            BuySellViewModel viewModel = new BuySellViewModel();
            viewModel.Player = Session["player"] as Player;

            return View("Buy", viewModel);
        }

        [HttpPost]
        public ActionResult Buy(BuySellViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Buy", model);
            }

            Player player = Session["player"] as Player;
            model.Player = player;

            if (player.Money < (player.Prices[model.PartName] * model.Quantity))
            {
                ModelState.AddModelError("notEnoughMoney", "You do not have enough money to complete this purchase");
                return View("Buy", model);
            }

            player.Inventory[model.PartName] += model.Quantity;
            Session["player"] = player;

            return RedirectToAction("Menu");
        }

        public ActionResult Sell()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            BuySellViewModel viewModel = new BuySellViewModel();
            viewModel.Player = Session["player"] as Player;

            return View("Sell", viewModel);
        }

        public ActionResult Casino()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Casino", Session["player"] as Player);
        }

        [HttpGet]
        public ActionResult Airport()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Airport", Session["player"] as Player);
        }

        public ActionResult NextDay()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            Player player = Session["player"] as Player;

            player.CurrentDay++;
            player.Prices = pricesDAL.RandomizePrices();
            Session["player"] = player;

            return RedirectToAction("Menu");
        }

        public ActionResult GameOver()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            Player player = Session["player"] as Player;
            Session.Abandon();

            return View("GameOver", player);
        }
    }
}