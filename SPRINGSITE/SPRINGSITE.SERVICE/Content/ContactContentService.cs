using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IContactContentService
    {
        IEnumerable<ContactContent> GetContactContents();

        ContactContent GetContactContent(int id);
        void CreateContactContent(ContactContent contactContent);
        void UpdateContactContent(ContactContent contactContent);
        void DeleteContactContent(int id);

        void Save();
    }

    public class ContactContentService : IContactContentService
    {
        private readonly IContactContentRepository _iContactContentRepository;
        private readonly IUnitOfWork _iUitOfWork;

        public ContactContentService(IContactContentRepository iContactContentRepository, IUnitOfWork iUnitOfWork)
        {
            this._iContactContentRepository = iContactContentRepository;
            this._iUitOfWork = iUnitOfWork;
        }

        public IEnumerable<ContactContent> GetContactContents()
        {
            var contactContents = _iContactContentRepository.GetAll();
            return contactContents;
        }

        public ContactContent GetContactContent(int id)
        {
            var contactContent = _iContactContentRepository.GetById(id);
            return contactContent;
        }

        public void CreateContactContent(ContactContent contactContent)
        {
            _iContactContentRepository.Add(contactContent);
            Save();
        }

        public void UpdateContactContent(ContactContent contactContent)
        {
            _iContactContentRepository.Update(contactContent);
            Save();
        }

        public void DeleteContactContent(int id)
        {
            var contactContent = GetContactContent(id);
            _iContactContentRepository.Delete(contactContent);
            Save();
        }

        public void Save()
        {
            _iUitOfWork.Commit();
        }

    }

}
