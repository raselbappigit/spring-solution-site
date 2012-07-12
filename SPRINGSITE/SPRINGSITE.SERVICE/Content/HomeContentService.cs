using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IHomeContentService
    {
        IEnumerable<HomeContent> GetHomeContents();

        HomeContent GetHomeContent(int id);
        void CreateHomeContent(HomeContent homeContent);
        void UpdateHomeContent(HomeContent homeContent);
        void DeleteHomeContent(int id);

        void Save();
    }

    public class HomeContentService : IHomeContentService
    {
        private readonly IHomeContentRepository _iHomeContentRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public HomeContentService(IHomeContentRepository iHomeContentRepository, IUnitOfWork iUnitOfWork)
        {
            this._iHomeContentRepository = iHomeContentRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<HomeContent> GetHomeContents()
        {
            var homeContents = _iHomeContentRepository.GetAll();
            return homeContents;
        }

        public HomeContent GetHomeContent(int id)
        {
            var homeContent = _iHomeContentRepository.GetById(id);
            return homeContent;
        }

        public void CreateHomeContent(HomeContent homeContent)
        {
            _iHomeContentRepository.Add(homeContent);
            Save();
        }

        public void UpdateHomeContent(HomeContent homeContent)
        {
            _iHomeContentRepository.Update(homeContent);
            Save();
        }

        public void DeleteHomeContent(int id)
        {
            var homeContent = GetHomeContent(id);
            _iHomeContentRepository.Delete(homeContent);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
