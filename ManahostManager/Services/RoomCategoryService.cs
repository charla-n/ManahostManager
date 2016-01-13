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
    public class RoomCategoryService : AbstractService<RoomCategory, RoomCategoryDTO>
    {
        private new IRoomCategoryRepository repo;

        public RoomCategoryService(IRoomCategoryRepository repo, IValidation<RoomCategory> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(RoomCategoryDTO dto, int id, Client currentClient)
        {
            orig = repo.GetRoomCategoryById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override RoomCategory DoPostPutDto(Client currentClient, RoomCategoryDTO dto, RoomCategory entity, string path, object param)
        {
            if (entity == null)
                entity = new RoomCategory();
            else
            {
                if (dto.RefHide == null)
                    dto.RefHide = entity.RefHide;
            }
            GetMapper.Map<RoomCategoryDTO, RoomCategory>(dto, entity);
            return entity;
        }

        protected override void SetDefaultValues(RoomCategory entity)
        {
            entity.RefHide = entity.RefHide ?? true;
        }
    }
}