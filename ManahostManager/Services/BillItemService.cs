using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class BillItemService : AbstractService<BillItem, BillItemDTO>
    {
        private new IBillItemRepository BillItemRepository { get; set; }

        public IAbstractService<Bill, BillDTO> BillService { get { return GetService<IAbstractService<Bill, BillDTO>>(); } private set { } }

        public IAbstractService<BillItemCategory, BillItemCategoryDTO> BillItemCategoryService { get { return GetService<IAbstractService<BillItemCategory, BillItemCategoryDTO>>(); } private set { } }

        public IAbstractService<GroupBillItem, GroupBillItemDTO> GroupBillItemService { get { return GetService<IAbstractService<GroupBillItem, GroupBillItemDTO>>(); } private set { } }

        public BillItemService(IBillItemRepository BillItemRepository, IValidation<BillItem> BillItemValidation) : base(BillItemValidation, BillItemRepository)
        {
            this.BillItemRepository = BillItemRepository;
        }

        public override void ProcessDTOPostPut(BillItemDTO dto, int id, Client currentClient)
        {
            orig = BillItemRepository.GetBillItemById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(BillItemDTO dto)
        {
            if (dto.Bill != null)
                BillItemRepository.includes.Add("Bill");
            if (dto.BillItemCategory != null)
                BillItemRepository.includes.Add("BillItemCategory");
            if (dto.GroupBillItem != null)
                BillItemRepository.includes.Add("GroupBillItem");
        }

        protected override BillItem DoPostPutDto(Client currentClient, BillItemDTO dto, BillItem entity, string path, object param)
        {
            if (entity == null)
                entity = new BillItem();
            GetMapper.Map(dto, entity);
            if (dto.Bill != null && dto.Bill.Id != null && dto.Bill.Id != 0)
                entity.Bill = BillService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.Bill, currentClient, path);
            if (dto.BillItemCategory != null)
                entity.BillItemCategory = BillItemCategoryService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BillItemCategory, currentClient, path);
            if (dto.GroupBillItem != null)
                entity.GroupBillItem = GroupBillItemService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.GroupBillItem, currentClient, path);
            return entity;
        }
    }
}