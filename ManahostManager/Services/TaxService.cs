using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class TaxService : AbstractService<Tax, TaxDTO>
    {
        private new ITaxRepository TaxRepository;


        //TODO REFRACTOR WHEN FINISH
        private IMapper MapperInstance
        {
            get
            {
                return GetMapper;
            }
        }

        public TaxService(ITaxRepository repo, IValidation<Tax> validation): base(validation, repo)
        {
            TaxRepository = repo;
        }

        public override void ProcessDTOPostPut(TaxDTO dto, int id, Client currentClient)
        {
            orig = TaxRepository.GetTaxById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override Tax DoPostPutDto(Client currentClient, TaxDTO dto, Tax entity, string path, object param)
        {
            if (entity == null)
                entity = new Tax();
            MapperInstance.Map<TaxDTO, Tax>(dto, entity);
            return entity;
        }
    }
}