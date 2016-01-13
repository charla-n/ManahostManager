using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class PeopleCategoryService : AbstractService<PeopleCategory, PeopleCategoryDTO>
    {
        private new IPeopleCategoryRepository repo;

        public IAbstractService<Tax, TaxDTO> TaxService { get { return GetService<IAbstractService<Tax, TaxDTO>>(); } private set { } }

        public PeopleCategoryService(IPeopleCategoryRepository repo, IValidation<PeopleCategory> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PeopleCategoryDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPeopleCategoryById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(PeopleCategoryDTO dto)
        {
            if (dto.Tax != null)
                repo.includes.Add("Tax");
        }

        protected override PeopleCategory DoPostPutDto(Client currentClient, PeopleCategoryDTO dto, PeopleCategory entity, string path, object param)
        {
            if (entity == null)
                entity = new PeopleCategory();
            GetMapper.Map(dto, entity);
            if (dto.Tax != null)
                entity.Tax = TaxService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Tax, currentClient, path);
            return entity;
        }
    }
}