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
    public class PeriodService : AbstractService<Period, PeriodDTO>
    {
        private new IPeriodRepository repo;

        public PeriodService(IPeriodRepository repo, IValidation<Period> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(PeriodDTO dto, int id, Client currentClient)
        {
            orig = repo.GetPeriodById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override Period DoPostPutDto(Client currentClient, PeriodDTO dto, Period entity, string path, object param)
        {
            if (entity == null)
                entity = new Period();
            GetMapper.Map(dto, entity);
            return entity;
        }
    }
}