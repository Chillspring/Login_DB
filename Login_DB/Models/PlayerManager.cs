using Login_DB.DataAccess;
using System.Collections.Generic;
using System.Linq;

namespace Login_DB.Models
{
    public class PlayerManager
    {
        private PlayerContext _playerDB = new PlayerContext();

        private Dictionary<string, string> _loggedIn = new Dictionary<string, string>();

        public bool Register(Player player, string sessionId, out string message)
        {
            message = "";

            Player selectedPlayer = _playerDB.Players.Where(p => p.Name == player.Name).FirstOrDefault();
            if (selectedPlayer != null)
            {
                message = "Name exists allready.";
                return false;
            }

            _playerDB.Players.Add(player);
            _playerDB.SaveChanges();

            _loggedIn.Add(sessionId, player.Name);

            return true;
        }

        public bool Login(Player player, string sessionId, out string message)
        {
            message = "";

            Player selectedPlayer = _playerDB.Players.Where(p => p.Name == player.Name).FirstOrDefault();

            if (selectedPlayer == null)
            {
                message = "An account with this name does not exist.";
                return false;
            }
            else if (selectedPlayer.Password != player.Password)
            {
                message = "The password is wrong.";
                return false;
            }

            _loggedIn.Add(sessionId, player.Name);

            return true;
        }

        public void Logout(string sessionId)
        {
            _loggedIn.Remove(sessionId);
        }

        public bool IsLoggedIn(string sessionId)
        {
            return _loggedIn.ContainsKey(sessionId);
        }

        public string GetCurrentLoggedInName(string sessionId)
        {
            if (_loggedIn.ContainsKey(sessionId))
            {
                return _loggedIn[sessionId];
            }
            else return "";
        }
    }
}