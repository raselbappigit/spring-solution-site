using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IAboutContentService
    {
        IEnumerable<AboutContent> GetAboutContents();

        AboutContent GetAboutContent(int id);
        void CreateAboutContent(AboutContent aboutContent);
        void UpdateAboutContent(AboutContent aboutContent);
        void DeleteAboutContent(int id);

        void Save();
    }

    public class AboutContentService : IAboutContentService
    {
        private readonly IAboutContentRepository _iAboutContentRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public AboutContentService(IAboutContentRepository iAboutContentRepository, IUnitOfWork iUnitOfWork)
        {
            this._iAboutContentRepository = iAboutContentRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<AboutContent> GetAboutContents()
        {
            var aboutContents = _iAboutContentRepository.GetAll();
            return aboutContents;
        }

        public AboutContent GetAboutContent(int id)
        {
            var aboutContent = _iAboutContentRepository.GetById(id);
            return aboutContent;
        }

        public void CreateAboutContent(AboutContent aboutContent)
        {
            _iAboutContentRepository.Add(aboutContent);
            Save();
        }

        public void UpdateAboutContent(AboutContent aboutContent)
        {
            _iAboutContentRepository.Update(aboutContent);
            Save();
        }

        public void DeleteAboutContent(int id)
        {
            var aboutContent = GetAboutContent(id);
            _iAboutContentRepository.Delete(aboutContent);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
