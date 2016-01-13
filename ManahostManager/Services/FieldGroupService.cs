using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class FieldGroupService : AbstractService<FieldGroup, FieldGroupDTO>
    {
        private new IFieldGroupRepository repo;

        public FieldGroupService(IFieldGroupRepository repo, IValidation<FieldGroup> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(FieldGroupDTO dto, int id, Client currentClient)
        {
            orig = repo.GetFieldGroupById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(FieldGroupDTO dto)
        {
            if (dto.PeopleFields != null)
                repo.includes.Add("PeopleFields");
        }

        
        public IPeopleFieldRepository PeopleFieldRepository { get { return GetService<IPeopleFieldRepository>(); } private set { } }

        
        public IAbstractService<PeopleField, PeopleFieldDTO> PeopleFieldService { get { return GetService<IAbstractService<PeopleField, PeopleFieldDTO>>(); } private set { } }

        protected override FieldGroup DoPostPutDto(Client currentClient, FieldGroupDTO dto, FieldGroup entity, string path, object param)
        {
            if (entity == null)
                entity = new FieldGroup();
            GetMapper.Map(dto, entity);
            if (dto.PeopleFields != null)
            {
                PeopleFieldRepository.DeleteRange(entity.PeopleFields.Where(d => !dto.PeopleFields.Any(x => x.Id == d.Id)));
                dto.PeopleFields.ForEach(peopleField =>
        {
            if (entity.PeopleFields.Count != 0 && peopleField.Id != 0 &&
            entity.PeopleFields.Find(p => p.Id == peopleField.Id) != null)
                return;
            PeopleField toAdd = PeopleFieldService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, peopleField, currentClient, path);

            if (toAdd != null)
                entity.PeopleFields.Add(toAdd);
        });
            }
            return entity;
        }
    }
}