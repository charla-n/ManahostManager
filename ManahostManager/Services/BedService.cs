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
    public class BedService : AbstractService<Bed, BedDTO>
    {
        private new IBedRepository repo;

        public IAbstractService<Room, RoomDTO> RoomService { get { return GetService<IAbstractService<Room, RoomDTO>>(); } private set { } }

        public BedService(IBedRepository repo, IValidation<Bed> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(BedDTO dto, int id, Client currentClient)
        {
            orig = repo.GetBedById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BedDTO dto)
        {
            if (dto.Room != null)
                repo.includes.Add("Room");
        }

        protected override Bed DoPostPutDto(Client currentClient, BedDTO dto, Bed entity, string path, object param)
        {
            if (entity == null)
                entity = new Bed();
            GetMapper.Map<BedDTO, Bed>(dto, entity);
            if (dto.Room != null)
                entity.Room = RoomService.PreProcessDTOPostPut(validationDictionnary ,dto.HomeId, dto.Room, currentClient, path);
            return entity;
        }
    }
}