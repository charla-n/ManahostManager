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
    public class RoomService : AbstractService<Room, RoomDTO>
    {
        private new IRoomRepository repo;

        public RoomService(IRoomRepository repo, IValidation<Room> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(RoomDTO dto, int id, Client currentClient)
        {
            orig = repo.GetRoomById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(RoomDTO dto)
        {
            if (dto.Beds != null)
                repo.includes.Add("Beds");
            if (dto.BillItemCategory != null)
                repo.includes.Add("BillItemCategory");
            if (dto.RoomCategory != null)
                repo.includes.Add("RoomCategory");
            if (dto.Documents != null)
                repo.includes.Add("Documents");
        }

        public IBedRepository BedRepository { get { return GetService<IBedRepository>(); } private set { } }
        public IDocumentRepository DocumentRepository { get { return GetService<IDocumentRepository>(); } private set { } }
        public IHomeConfigRepository HomeConfigRepository { get { return GetService<IHomeConfigRepository>(); } private set { } }
        public IAbstractService<BillItemCategory, BillItemCategoryDTO> BillItemCategoryService { get { return GetService<IAbstractService<BillItemCategory, BillItemCategoryDTO>>(); } private set { } }
        public IAbstractService<RoomCategory, RoomCategoryDTO> RoomCategoryService { get { return GetService<IAbstractService<RoomCategory, RoomCategoryDTO>>(); } private set { } }
        public IAbstractService<Bed, BedDTO> BedService { get { return GetService<IAbstractService<Bed, BedDTO>>(); } private set { } }
        public IAdditionnalDocumentMethod DocumentService { get { return GetService<IAdditionnalDocumentMethod>(); } private set { } }

        protected override Room DoPostPutDto(Client currentClient, RoomDTO dto, Room entity, string path, object param)
        {
            if (entity == null)
                entity = new Room();
            else
            {
                if (dto.RefHide == null)
                    dto.RefHide = entity.RefHide;
                if (dto.Color == null)
                    dto.Color = entity.Color;
            }
            GetMapper.Map<RoomDTO, Room>(dto, entity);
            if (dto.BillItemCategory != null)
                entity.BillItemCategory = BillItemCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BillItemCategory, currentClient, path);
            if (dto.RoomCategory != null)
                entity.RoomCategory = RoomCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.RoomCategory, currentClient, path);
            if (dto.Beds != null)
            {
                BedRepository.DeleteRange(entity.Beds.Where(d => !dto.Beds.Any(x => x.Id == d.Id)));
                dto.Beds.ForEach(bed =>
                    {
                        if (entity.Beds.Count != 0 && bed.Id != 0 &&
                        entity.Beds.Find(p => p.Id == bed.Id) != null)
                            return;
                        Bed toAdd = BedService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, bed, currentClient, path);

                        if (toAdd != null)
                            entity.Beds.Add(toAdd);
                    });
            }
            if (dto.Documents != null)
            {
                DocumentRepository.DeleteRange(entity.Documents.Where(d => !dto.Documents.Any(x => x.Id == d.Id)));
                dto.Documents.ForEach(document =>
        {
            Document toAdd = DocumentService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, document, currentClient, path);

            if (toAdd != null)
                entity.Documents.Add(toAdd);
        });
            }
            return entity;
        }

        protected override void SetDefaultValues(Room entity)
        {
            entity.RefHide = entity.RefHide ?? true;
            entity.Color = entity.Color ?? 0xFFFFFF;
        }

        protected override void DoPost(Client currentClient, Room entity, object param)
        {
            HomeConfig config;

            HomeConfigRepository.includes.Add("DefaultBillItemCategoryRoom");
            config = HomeConfigRepository.GetHomeConfigById(entity.HomeId, currentClient.Id);
            if (config != null && config.DefaultBillItemCategoryRoom != null && entity.BillItemCategory == null)
                entity.BillItemCategory = config.DefaultBillItemCategoryRoom;
        }
    }
}