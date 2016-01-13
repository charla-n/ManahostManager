using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;

namespace ManahostManager.Services
{
    public class GroupBillItemService : AbstractService<GroupBillItem, GroupBillItemDTO>
    {
        private new IGroupBillItemRepository GroupBillItemRepository;

        public GroupBillItemService(IValidation<GroupBillItem> GroupBillItemValidation, IGroupBillItemRepository GroupBillItemRepository) : base(GroupBillItemValidation, GroupBillItemRepository)
        {
            this.GroupBillItemRepository = GroupBillItemRepository;
        }

        public override void ProcessDTOPostPut(GroupBillItemDTO dto, int id, Client currentClient)
        {
            orig = GroupBillItemRepository.GroupBillItemById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override GroupBillItem DoPostPutDto(Client currentClient, GroupBillItemDTO dto, GroupBillItem entity, string path, object param)
        {
            if (entity == null)
                entity = new GroupBillItem();
            GetMapper.Map(dto, entity);
            return entity;
        }
    }
}