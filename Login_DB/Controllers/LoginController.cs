using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Login_DB.Models;
using Login_DB.DataAccess;
using System.Text.RegularExpressions;

namespace Login_DB.Controllers
{
    public class LoginController : Controller
    {
        PlayerManager _playerManager = new PlayerManager();

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
                    return RedirectToAction("InGame");
                }
            }
            return View();
        }

        public ActionResult InGame()
        {
            return View(new PlayerContext().Players.ToList());
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
                    return RedirectToAction("InGame");
                }
            }
            return View();
        }
    }
}