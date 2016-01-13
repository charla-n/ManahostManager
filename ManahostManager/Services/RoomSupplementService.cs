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
    public class RoomSupplementService : AbstractService<RoomSupplement, RoomSupplementDTO>
    {
        private new IRoomSupplementRepository repo;

        public IAbstractService<Tax, TaxDTO> TaxService { get { return GetService<IAbstractService<Tax, TaxDTO>>(); } private set { } }

        public RoomSupplementService(IRoomSupplementRepository repo, IValidation<RoomSupplement> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(RoomSupplementDTO dto, int id, Client currentClient)
        {
            orig = repo.GetRoomSupplementById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(RoomSupplementDTO dto)
        {
            if (dto.Tax != null)
                repo.includes.Add("Tax");
        }

        protected override RoomSupplement DoPostPutDto(Client currentClient, RoomSupplementDTO dto, RoomSupplement entity, string path, object param)
        {
            if (entity == null)
                entity = new RoomSupplement();
            GetMapper.Map(dto, entity);
            if (dto.Tax != null)
                entity.Tax = TaxService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Tax, currentClient, path);
            return entity;
        }
    }
}