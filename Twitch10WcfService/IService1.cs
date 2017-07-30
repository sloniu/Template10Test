using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Twitch10WcfService.DAL.Entities;

namespace Twitch10WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region Builds

        [OperationContract]
        IQueryable<Build> GetBuilds();

        [OperationContract]
        Build Get(string region, string name);

        [OperationContract]
        Build GetBuildById(int id);

        [OperationContract]
        void PostBuild(string token, string region, string playerName, string matchId);

        [OperationContract]
        void DeleteBuild(int id);

        #endregion

        #region Users

        [OperationContract]
        IQueryable<User> GetUser();

        [OperationContract]
        User GetUserById(int id);

        [OperationContract]
        void PostUser(string userName, string token);

        #endregion
    }
}