using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http.ModelBinding;
using ManahostManager.Utils.Extension;

namespace ManahostManager.Services
{
    public class HomeService : AbstractService<Home, HomeDTO>
    {
        private readonly int SIZE_ENCRYPTION_PASSWORD = 16;
        private new IHomeRepository repo;

        public HomeService(IHomeRepository repo, IValidation<Home> validation) : base(validation, repo)
        {
            repo.includes = new List<string>();
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(HomeDTO dto, int id, Client currentClient)
        {
            orig = repo.GetHomeById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override Home DoPostPutDto(Client currentClient, HomeDTO dto, Home entity, string path, object param)
        {
            if (entity == null)
                entity = new Home();
            GetMapper.Map(dto, entity);
            return entity;
        }

        protected override void DoPost(Client currentClient, Home entity, object param)
        {
            entity.ClientId = currentClient.Id;
            entity.EncryptionPassword = Guid.NewGuid().ToString("d").Substring(1, SIZE_ENCRYPTION_PASSWORD);
            entity.isDefault = false;
        }

        public override IEnumerable<HomeDTO> DoGet(Client current, object param)
        {
            IEnumerable<Home> homes = repo.GetHomesForClient(current.Id);
            List<HomeDTO> lhomes = new List<HomeDTO>();

            foreach (Home cur in homes)
            {
                lhomes.Add(GetMapper.Map<Home, HomeDTO>(cur));
            }
            return lhomes;
        }

        public override HomeDTO DoGet(Client current, int homeId, object param)
        {
            Home home = repo.GetHomeById(homeId, current.Id);

            if (home == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<Home>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                throw new ManahostValidationException(validationDictionnary);
            }
            return GetMapper.Map<Home, HomeDTO>(home);
        }

        protected override void DoDelete(Client currentClient, int id, object param)
        {
            Home home = repo.GetHomeById(id, currentClient.Id);

            if (home == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<Home>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                throw new ManahostValidationException(validationDictionnary);
            }
            if (currentClient.DefaultHomeId == id)
            {
                currentClient.DefaultHomeId = null;
                repo.Update<Client>(currentClient);
            }
            repo.Delete(home);
            repo.Save();
        }

        public async Task ChangeDefaultHomeId(Client currentClient, int HomeId)
        {
            Home HomeSelectedForChange = repo.GetHomeById(HomeId, currentClient.Id);
            if (HomeSelectedForChange == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<Home>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                throw new ManahostValidationException(validationDictionnary);
            }

            Home HomeOnIsDefault = repo.GetUniq(x => x.Id != HomeId && x.isDefault == true);
            if (HomeOnIsDefault != null)
            {
                HomeOnIsDefault.isDefault = false;
                repo.Update(HomeOnIsDefault);
            }
            HomeSelectedForChange.isDefault = true;
            repo.Update(HomeSelectedForChange);
            await repo.SaveAsync();
        }

        public HomeDTO DoGetDefaultHome(Client currentClient)
        {
            Home home = repo.GetUniq(x => x.ClientId == currentClient.Id && x.isDefault == true);
            if (home == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<Home>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                throw new ManahostValidationException(validationDictionnary);
            }
            return GetMapper.Map<Home, HomeDTO>(home);
        }
    }
}