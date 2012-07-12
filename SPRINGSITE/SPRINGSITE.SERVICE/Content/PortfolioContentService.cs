using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IPortfolioContentService
    {
        IEnumerable<PortfolioContent> GetPortfolioContents();

        PortfolioContent GetPortfolioContent(int id);
        void CreatePortfolioContent(PortfolioContent portfolioContent);
        void UpdatePortfolioContent(PortfolioContent portfolioContent);
        void DeletePortfolioContent(int id);

        void Save();
    }

    public class PortfolioContentService : IPortfolioContentService
    {
        private readonly IPortfolioContentRepository _iPortfolioContentRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public PortfolioContentService(IPortfolioContentRepository iPortfolioContentRepository, IUnitOfWork iUnitOfWork)
        {
            this._iPortfolioContentRepository = iPortfolioContentRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<PortfolioContent> GetPortfolioContents()
        {
            var portfolioContents = _iPortfolioContentRepository.GetAll();
            return portfolioContents;
        }

        public PortfolioContent GetPortfolioContent(int id)
        {
            var portfolioContent = _iPortfolioContentRepository.GetById(id);
            return portfolioContent;
        }

        public void CreatePortfolioContent(PortfolioContent portfolioContent)
        {
            _iPortfolioContentRepository.Add(portfolioContent);
            Save();
        }

        public void UpdatePortfolioContent(PortfolioContent portfolioContent)
        {
            _iPortfolioContentRepository.Update(portfolioContent);
            Save();
        }

        public void DeletePortfolioContent(int id)
        {
            var portfolioContent = GetPortfolioContent(id);
            _iPortfolioContentRepository.Delete(portfolioContent);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
