using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;
using System.Collections.Generic;
using ManahostManager.Utils;

namespace ManahostManager.Services
{
    public class PeopleFieldService : AbstractService<PeopleField, PeopleFieldDTO>
    {
        private new IPeopleFieldRepository repo;

        
        public IAbstractService<FieldGroup, FieldGroupDTO> FieldGroupService { get { return GetService<IAbstractService<FieldGroup, FieldGroupDTO>>(); } private set { } }

        
        public IAbstractService<People, PeopleDTO> PeopleService { get { return GetService<IAbstractService<People, PeopleDTO>>(); } private set { } }

        
        public IPeopleFieldRepository PeopleFieldRepository { get { return GetService<IPeopleFieldRepository>(); } private set { } }

        
        public IPeopleRepository PeopleRepository { get { return GetService<IPeopleRepository>(); } private set { } }

        public PeopleFieldService(IPeopleFieldRepository repo, IValidation<PeopleField> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PeopleFieldDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPeopleFieldById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(PeopleFieldDTO dto)
        {
            if (dto.FieldGroup != null)
                repo.includes.Add("FieldGroup");
            if (dto.People != null)
                repo.includes.Add("People");
        }

        protected override PeopleField DoPostPutDto(Client currentClient, PeopleFieldDTO dto, PeopleField entity, string path, object param)
        {
            if (entity == null)
                entity = new PeopleField();
            GetMapper.Map(dto, entity);
            if (dto.FieldGroup != null)
                entity.FieldGroup = FieldGroupService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.FieldGroup, currentClient, path);
            if (dto.People != null)
                entity.People = PeopleService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.People, currentClient, path);
            return entity;
        }

        virtual public void PostFieldForFieldGroup(Client currentClient, int FieldGroupId, int PeopleId)
        {
            if (!((PeopleFieldValidation)validation).PostFieldValidation(validationDictionnary, currentClient, FieldGroupId, PeopleId))
                throw new ManahostValidationException(validationDictionnary);
            repo.includes.Add("FieldGroup");
            repo.includes.Add("People");
            IEnumerable<PeopleField> fields = PeopleFieldRepository.GetPeopleFieldsByFieldGroup(FieldGroupId, currentClient.Id);

            foreach (PeopleField curr in fields)
            {
                curr.FieldGroup = null;
                curr.People = PeopleRepository.GetPeopleById(PeopleId, currentClient.Id);
                PeopleRepository.Add(curr);
            }
            PeopleRepository.Save();
        }
    }
}