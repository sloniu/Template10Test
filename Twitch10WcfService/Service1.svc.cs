using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Twitch10WcfService.DAL;
using Twitch10WcfService.DAL.Entities;
using Twitch10WcfService.Exceptions;
using Twitch10WcfService.Models;

namespace Twitch10WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private readonly TwitchContext _dbContext = new TwitchContext();

        public List<BuildContract> GetBuilds()
        {
            if (_dbContext == null)
            {
                var fault = new ServiceException
                {
                    Id = 1,
                    Message = "Database exception",
                    Description = "_dbContext is null"
                };
                throw new FaultException<ServiceException>(fault);
            }
            var builds = _dbContext.Builds;
            var result = builds.Select(build => new BuildContract
            {
                BuildId = build.BuildId,
                ChampionId = build.ChampionId,
                Item1Id = build.Item1Id,
                Item2Id = build.Item2Id,
                Item3Id = build.Item3Id,
                Item4Id = build.Item4Id,
                Item5Id = build.Item5Id,
                Item6Id = build.Item6Id,
                MatchId = build.MatchId,
                PlayerName = build.PlayerName,
                Spell1Id = build.Spell1Id,
                Spell2Id = build.Spell2Id,
                Region = build.Region
            }).ToList();
            return result.ToList();
        }

        public BuildContract Get(string region, string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(region))
            {
                var fault = new ServiceException
                {
                    Id = 2,
                    Message = "Data exception",
                    Description = "region or name is null or empty"
                };
                throw new FaultException<ServiceException>(fault);
            }
            var parser = new JsonParser();
            var build = parser.GetLastBuild(region, name);
            var result = new BuildContract
            {
                BuildId = build.BuildId,
                ChampionId = build.ChampionId,
                Item1Id = build.Item1Id,
                Item2Id = build.Item2Id,
                Item3Id = build.Item3Id,
                Item4Id = build.Item4Id,
                Item5Id = build.Item5Id,
                Item6Id = build.Item6Id,
                MatchId = build.MatchId,
                PlayerName = build.PlayerName,
                Spell1Id = build.Spell1Id,
                Spell2Id = build.Spell2Id,
                Region = region
            };

            return result;
        }

        public BuildContract GetBuildById(int id)
        {
            var build = _dbContext.Builds.Find(id);
            var result = new BuildContract
            {
                BuildId = build.BuildId,
                ChampionId = build.ChampionId,
                Item1Id = build.Item1Id,
                Item2Id = build.Item2Id,
                Item3Id = build.Item3Id,
                Item4Id = build.Item4Id,
                Item5Id = build.Item5Id,
                Item6Id = build.Item6Id,
                MatchId = build.MatchId,
                PlayerName = build.PlayerName,
                Spell1Id = build.Spell1Id,
                Spell2Id = build.Spell2Id,
                Region = build.Region
            };

            return result;
        }

        public void PostBuild(string token, string region, string playerName, string matchId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(region) || string.IsNullOrEmpty(playerName) || string.IsNullOrEmpty(matchId))
            {
                var fault = new ServiceException
                {
                    Id = 3,
                    Message = "Data exception",
                    Description = "region or token or playername or matchid is null or empty"
                };
                throw new FaultException<ServiceException>(fault);
            }
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
            try
            {
                var build = _dbContext.Builds.Find(id);
                if (build == null)
                    return;
                _dbContext.Builds.Remove(build);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                var fault = new ServiceException
                {
                    Id = 4,
                    Message = "Delete exception",
                    Description = $"region or token or playername or matchid is null or empty, {e.Message}"
                };
                throw new FaultException<ServiceException>(fault);
            }
            
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
            var user = new User
            {
                UserName = userName
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public List<BuildContract> GetBuildsByUser(string token)
        {
            if (!AuthenticateToken.Instance.Authenticate(token)) return null;
            var builds =
                _dbContext.Users.Where(u => u.UserName == AuthenticateToken.Instance.UserName)
                    .SelectMany(u => u.UserHasBuilds.Select(uhb => uhb.Build))
                    .ToList();
            var result = builds.Select(build => new BuildContract
            {
                BuildId = build.BuildId,
                ChampionId = build.ChampionId,
                Item1Id = build.Item1Id,
                Item2Id = build.Item2Id,
                Item3Id = build.Item3Id,
                Item4Id = build.Item4Id,
                Item5Id = build.Item5Id,
                Item6Id = build.Item6Id,
                MatchId = build.MatchId,
                PlayerName = build.PlayerName,
                Spell1Id = build.Spell1Id,
                Spell2Id = build.Spell2Id,
                Region = build.Region
            }).ToList();
            return result;
        }
    }
}