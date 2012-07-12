using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPRINGSITE.DOMAIN;
using SPRINGSITE.DATA;

namespace SPRINGSITE.SERVICE
{
    public interface IProfileService
    {
        IEnumerable<Profile> GetProfiles();

        Profile GetProfile(int id);
        void CreateProfile(Profile profile);
        void UpdateProfile(Profile profile);
        void DeleteProfile(int id);

        void Save();
    }

    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _iProfileRepository;
        private readonly IUnitOfWork _iUnitOfWork;

        public ProfileService(IProfileRepository iProfileRepository, IUnitOfWork iUnitOfWork)
        {
            this._iProfileRepository = iProfileRepository;
            this._iUnitOfWork = iUnitOfWork;
        }

        public IEnumerable<Profile> GetProfiles()
        {
            var profiles = _iProfileRepository.GetAll();
            return profiles;
        }

        public Profile GetProfile(int id)
        {
            var profile = _iProfileRepository.GetById(id);
            return profile;
        }

        public void CreateProfile(Profile profile)
        {
            _iProfileRepository.Add(profile);
            Save();
        }

        public void UpdateProfile(Profile profile)
        {
            _iProfileRepository.Update(profile);
            Save();
        }

        public void DeleteProfile(int id)
        {
            var profile = GetProfile(id);
            _iProfileRepository.Delete(profile);
            Save();
        }

        public void Save()
        {
            _iUnitOfWork.Commit();
        }

    }
}
