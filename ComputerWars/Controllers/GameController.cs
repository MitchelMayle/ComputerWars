using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ComputerWars.Models;
using ComputerWars.DAL;

namespace ComputerWars.Controllers
{
    [HandleError]
    public class GameController : Controller
    {
        private readonly IPricesDAL pricesDAL;
        private readonly IScoreDAL scoreSqlDAL;

        public GameController(IPricesDAL pricesDAL, IScoreDAL scoreSqlDAL)
        {
            this.pricesDAL = pricesDAL;
            this.scoreSqlDAL = scoreSqlDAL;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        [Route ("NewGame")]
        public ActionResult NewGame()
        {
            if (Session["player"] != null)
            {
                return RedirectToAction("Menu");
            }

            return View("NewGame");
        }

        [HttpPost]
        [Route("NewGame")]
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

        [Route ("Menu")]
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

        [Route("Prices")]
        public ActionResult Prices()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Prices", Session["player"] as Player);
        }

        [Route("Inventory")]
        public ActionResult Inventory()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Inventory", Session["player"] as Player);
        }

        [HttpGet]
        [Route("Buy")]
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
        [Route("Buy")]
        public ActionResult Buy(BuySellViewModel model)
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            Player player = Session["player"] as Player;
            model.Player = player;

            if (!ModelState.IsValid)
            {
                return View("Buy", model);
            }

            if (player.Money < (player.Prices[model.PartName] * model.Quantity))
            {
                ModelState.AddModelError("notEnoughMoney", "You do not have enough money.");
                return View("Buy", model);
            }

            player.Inventory[model.PartName] += model.Quantity;
            player.Money -= (player.Prices[model.PartName] * model.Quantity);
            Session["player"] = player;

            return RedirectToAction("Menu");
        }

        [HttpGet]
        [Route("Sell")]
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

        [HttpPost]
        [Route("Sell")]
        public ActionResult Sell(BuySellViewModel model)
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            Player player = Session["player"] as Player;
            model.Player = player;

            if (!ModelState.IsValid)
            {
                return View("Sell", model);
            }

            if (player.Inventory[model.PartName] < model.Quantity)
            {
                ModelState.AddModelError("notEnoughParts", "You do not have that many.");
                return View("Sell", model);
            }

            player.Inventory[model.PartName] -= model.Quantity;
            player.Money += (player.Prices[model.PartName] * model.Quantity);
            Session["player"] = player;

            return RedirectToAction("Menu");
        }

        [Route("Casino")]
        public ActionResult Casino()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Casino", Session["player"] as Player);
        }

        [Route("Airport")]
        public ActionResult Airport()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            return View("Airport", Session["player"] as Player);
        }

        [Route("NextDay")]
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

        [Route("HighScores")]
        public ActionResult HighScores()
        {
            List<HighScore> scores = scoreSqlDAL.TopScores();

            return View("HighScores", scores);
        }

        [Route("GameOver")]
        public ActionResult GameOver()
        {
            if (Session["player"] == null)
            {
                return RedirectToAction("Index");
            }

            Player player = Session["player"] as Player;
            Session.Abandon();

            if (player.Money > 1000)
            {
                scoreSqlDAL.SaveScore(player);
            }

            return View("GameOver", player);
        }
    }
}