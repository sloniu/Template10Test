using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using Twitch10WcfService.DAL.Entities;
using Twitch10WcfService.Exceptions;
using Twitch10WcfService.Models;

namespace Twitch10WcfService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        #region Builds

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        List<BuildContract> GetBuilds();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        BuildContract Get(string region, string name);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        BuildContract GetBuildById(int id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PostBuild(string token, string region, string playerName, string matchId);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void DeleteBuild(int id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        List<BuildContract> GetBuildsByUser(string token);
        #endregion

        #region Users

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        IQueryable<User> GetUser();

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        User GetUserById(int id);

        [OperationContract]
        [FaultContract(typeof(ServiceException))]
        [TransactionFlow(TransactionFlowOption.Allowed)]
        void PostUser(string userName, string token);

        #endregion
    }
}