using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IContactInfoService
    {
        IEnumerable<ContactInfo> GetContactInfos();

        ContactInfo GetContactInfo(int id);
        void CreateContactInfo(ContactInfo contactInfo);
        void UpdateContactInfo(ContactInfo contactInfo);
        void DeleteContactInfo(int id);

        void Save();
    }

    public class ContactInfoService : IContactInfoService
    {
        private readonly IContactInfoRepository _iContactInfoRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public ContactInfoService(IContactInfoRepository iContactInfoRepository, IUnitOfWork iUnitOfWork)
        {
            this._iContactInfoRepository = iContactInfoRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<ContactInfo> GetContactInfos()
        {
            var contactInfos = _iContactInfoRepository.GetAll();
            return contactInfos;
        }

        public ContactInfo GetContactInfo(int id)
        {
            var contactInfo = _iContactInfoRepository.GetById(id);
            return contactInfo;
        }

        public void CreateContactInfo(ContactInfo contactInfo)
        {
            _iContactInfoRepository.Add(contactInfo);
            Save();
        }

        public void UpdateContactInfo(ContactInfo contactInfo)
        {
            _iContactInfoRepository.Update(contactInfo);
            Save();
        }

        public void DeleteContactInfo(int id)
        {
            var contactInfo = GetContactInfo(id);
            _iContactInfoRepository.Delete(contactInfo);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
