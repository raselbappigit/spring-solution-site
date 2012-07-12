using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IBannerContentService
    {
        IEnumerable<BannerContent> GetBannerContents();

        BannerContent GetBannerContent(int id);
        void CreateBannerContent(BannerContent bannerContent);
        void UpdateBannerContent(BannerContent bannerContent);
        void DeleteBannerContent(int id);

        void Save();
    }

    public class BannerContentService : IBannerContentService
    {
        private readonly IBannerContentRepository _iBannerContentRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public BannerContentService(IBannerContentRepository iBannerContentRepository, IUnitOfWork iUnitOfWork)
        {
            this._iBannerContentRepository = iBannerContentRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<BannerContent> GetBannerContents()
        {
            var bannerContents = _iBannerContentRepository.GetAll();
            return bannerContents;
        }

        public BannerContent GetBannerContent(int id)
        {
            var bannerContent = _iBannerContentRepository.GetById(id);
            return bannerContent;
        }

        public void CreateBannerContent(BannerContent bannerContent)
        {
            _iBannerContentRepository.Add(bannerContent);
            Save();
        }

        public void UpdateBannerContent(BannerContent bannerContent)
        {
            _iBannerContentRepository.Update(bannerContent);
            Save();
        }

        public void DeleteBannerContent(int id)
        {
            var bannerContent = GetBannerContent(id);
            _iBannerContentRepository.Delete(bannerContent);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
