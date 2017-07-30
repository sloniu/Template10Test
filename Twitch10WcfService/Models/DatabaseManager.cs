using System.Linq;
using Twitch10WcfService.DAL;
using Twitch10WcfService.DAL.Entities;

namespace Twitch10WcfService.Models
{
    public class DatabaseManager
    {
        private readonly TwitchContext _dbContext = new TwitchContext();

        public void AddBuild(User user, Build build)
        {
            var a = _dbContext.Builds.Count(b => b.PlayerName == build.PlayerName && b.MatchId == build.MatchId);

            if (a != 0) return;
            _dbContext.Builds.Add(build);
            var u = new UserHasBuild()
            {
                UserId = user.UserId
            };
            _dbContext.UserHasBuilds.Add(u);
            _dbContext.SaveChanges();
        }

        public void AddUser(string userName)
        {
            var user = new User()
            {
                UserName = userName
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}