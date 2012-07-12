using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IQuickMailInfoService
    {
        IEnumerable<QuickMailInfo> GetQuickMailInfos();

        QuickMailInfo GetQuickMailInfo(int id);
        void CreateQuickMailInfo(QuickMailInfo quickMailInfo);
        void UpdateQuickMailInfo(QuickMailInfo quickMailInfo);
        void DeleteQuickMailInfo(int id);

        void Save();
    }

    public class QuickMailInfoService : IQuickMailInfoService
    {
        private readonly IQuickMailInfoRepository _iQuickMailInfoRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public QuickMailInfoService(IQuickMailInfoRepository iQuickMailInfoRepository, IUnitOfWork iUnitOfWork)
        {
            this._iQuickMailInfoRepository = iQuickMailInfoRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<QuickMailInfo> GetQuickMailInfos()
        {
            var quickMailInfos = _iQuickMailInfoRepository.GetAll();
            return quickMailInfos;
        }

        public QuickMailInfo GetQuickMailInfo(int id)
        {
            var quickMailInfo = _iQuickMailInfoRepository.GetById(id);
            return quickMailInfo;
        }

        public void CreateQuickMailInfo(QuickMailInfo quickMailInfo)
        {
            _iQuickMailInfoRepository.Add(quickMailInfo);
            Save();
        }

        public void UpdateQuickMailInfo(QuickMailInfo quickMailInfo)
        {
            _iQuickMailInfoRepository.Update(quickMailInfo);
            Save();
        }

        public void DeleteQuickMailInfo(int id)
        {
            var quickMailInfo = GetQuickMailInfo(id);
            _iQuickMailInfoRepository.Delete(quickMailInfo);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
