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
    public class SupplierService : AbstractService<Supplier, SupplierDTO>
    {
        private new ISupplierRepository repo;

        public SupplierService(ISupplierRepository repo, IValidation<Supplier> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(SupplierDTO dto, int id, Client currentClient)
        {
            orig = repo.GetSupplierById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override Supplier DoPostPutDto(Client currentClient, SupplierDTO dto, Supplier entity, string path, object param)
        {
            if (entity == null)
                entity = new Supplier();
            else
            {
                dto.DateCreation = entity.DateCreation;
            }
            GetMapper.Map(dto, entity);
            return entity;
        }

        protected override void SetDefaultValues(Supplier entity)
        {
            entity.DateCreation = DateTime.UtcNow;
        }
    }
}