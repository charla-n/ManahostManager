using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class PeopleService : AbstractService<People, PeopleDTO>
    {
        private new IPeopleRepository repo;

        public PeopleService(IPeopleRepository repo, IValidation<People> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PeopleDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPeopleById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override People DoPostPutDto(Client currentClient, PeopleDTO dto, People entity, string path, object param)
        {
            if (entity == null)
                entity = new People();
            GetMapper.Map<PeopleDTO, People>(dto, entity);
            return entity;
        }

        protected override void SetDefaultValues(People entity)
        {
            entity.DateCreation = DateTime.UtcNow;
        }
    }
}