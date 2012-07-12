using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IClientInfoService
    {
        IEnumerable<ClientInfo> GetClientInfos();

        ClientInfo GetClientInfo(int id);
        void CreateClientInfo(ClientInfo clientInfo);
        void UpdateClientInfo(ClientInfo clientInfo);
        void DeleteClientInfo(int id);

        void Save();
    }

    public class ClientInfoService : IClientInfoService
    {
        private readonly IClientInfoRepository _iClientInfoRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public ClientInfoService(IClientInfoRepository iClientInfoRepository, IUnitOfWork iUnitOfWork)
        {
            this._iClientInfoRepository = iClientInfoRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<ClientInfo> GetClientInfos()
        {
            var clientInfos = _iClientInfoRepository.GetAll();
            return clientInfos;
        }

        public ClientInfo GetClientInfo(int id)
        {
            var clientInfo = _iClientInfoRepository.GetById(id);
            return clientInfo;
        }

        public void CreateClientInfo(ClientInfo clientInfo)
        {
            _iClientInfoRepository.Add(clientInfo);
            Save();
        }

        public void UpdateClientInfo(ClientInfo clientInfo)
        {
            _iClientInfoRepository.Update(clientInfo);
            Save();
        }

        public void DeleteClientInfo(int id)
        {
            var clientInfo = GetClientInfo(id);
            _iClientInfoRepository.Delete(clientInfo);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
