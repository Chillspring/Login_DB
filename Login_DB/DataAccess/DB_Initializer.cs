using Login_DB.Models;
using System.Collections.Generic;

namespace Login_DB.DataAccess
{
    public class DB_Initializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<PlayerContext>
    {
        protected override void Seed(PlayerContext context)
        {
            var players = new List<Player>
            {
            };

            players.ForEach(s => context.Players.Add(s));
            context.SaveChanges();
        }
    }
}