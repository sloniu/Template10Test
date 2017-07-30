using System;
using System.Linq;
using Twitch10WcfService.DAL;
using Twitch10WcfService.DAL.Entities;
using Twitch10WcfService.Models;

namespace Twitch10WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private readonly TwitchContext _dbContext = new TwitchContext();
        public IQueryable<Build> GetBuilds()
        {
            return _dbContext.Builds;
        }

        public Build Get(string region, string name)
        {
            var parser = new JsonParser();
            return parser.GetLastBuild(region,name);
        }

        public Build GetBuildById(int id)
        {
            var build = _dbContext.Builds.Find(id);
            return build;
        }

        public void PostBuild(string token, string region, string playerName, string matchId)
        {
            var dbm = new DatabaseManager();
            AuthenticateToken.Instance.Authenticate(token);
            var user = _dbContext.Users.FirstOrDefault(u => u.UserName == AuthenticateToken.Instance.UserName);
            if (user == null)
            {
                dbm.AddUser(AuthenticateToken.Instance.UserName);
                user = _dbContext.Users.FirstOrDefault(u => u.UserName == AuthenticateToken.Instance.UserName);
            }
            var build = JsonParser.GetBuild(region, playerName, Convert.ToInt64(matchId));

            dbm.AddBuild(user, build);
        }

        public void DeleteBuild(int id)
        {
            var build = _dbContext.Builds.Find(id);
            if (build == null)
            {
                return;
            }
            _dbContext.Builds.Remove(build);
            _dbContext.SaveChanges();
        }

        public IQueryable<User> GetUser()
        {
            return _dbContext.Users;
        }

        public User GetUserById(int id)
        {
            var user = _dbContext.Users.Find(id);
            return user;
        }

        public void PostUser(string userName, string token)
        {
            if (!AuthenticateToken.Instance.Authenticate(token)) return;
            var user = new User()
            {
                UserName = userName
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}