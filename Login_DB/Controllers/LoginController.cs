using System.Linq;
using System.Web.Mvc;
using Login_DB.Models;
using Login_DB.DataAccess;
using System;

namespace Login_DB.Controllers
{
    public class LoginController : Controller
    {
        PlayerManager _playerManager = PlayerManager.Instance;

        // GET: Player
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Name,Password")] Player player)
        {
            if (ModelState.IsValid)
            {
                if (player == null)
                {
                    return HttpNotFound();
                }

                bool loggedIn = _playerManager.Login(player, Session.SessionID, out string message);
                ViewBag.Message = message;

                if (loggedIn)
                {
                    Session[player.Name] = player.Name;
                    return RedirectToAction("InGame");
                }
            }
            return View();
        }

        public ActionResult InGame()
        {
            if (_playerManager.IsLoggedIn(Session.SessionID)){
                return View(new PlayerContext().Players.ToList());
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult Logout_Click(object sender, EventArgs e)
        {
            _playerManager.Logout(Session.SessionID);
            return RedirectToAction("Index");
        }

        // GET: Player/Create
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Name,Password")] Player player)
        {
            if (ModelState.IsValid)
            {
                if (player == null)
                {
                    return HttpNotFound();
                }
                bool registered = _playerManager.Register(player, Session.SessionID, out string message);
                ViewBag.Message = message;
                if (registered)
                {
                    Session[player.Name] = player.Name;
                    return RedirectToAction("InGame");
                }
            }
            return View();
        }

    }
}