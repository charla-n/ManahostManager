using ManahostManager.App_Start;
using ManahostManager.Controllers;
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
    public class HomeConfigService : AbstractService<HomeConfig, HomeConfigDTO>
    {
        private static String BASIC_DEVISE = "€";
        private new IHomeConfigRepository repo;

        
        public IAdditionnalDocumentMethod DocumentService { get { return GetService<IAdditionnalDocumentMethod>(); } private set { } }

        
        public IAbstractService<MailConfig, MailConfigDTO> MailConfigService { get { return GetService<IAbstractService<MailConfig, MailConfigDTO>>(); } private set { } }

        public HomeConfigService(IHomeConfigRepository repo, IValidation<HomeConfig> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(HomeConfigDTO dto, int id, Client currentClient)
        {
            orig = repo.GetHomeConfigById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override void PutIncludeProps(HomeConfigDTO dto)
        {
            if (dto.BookingCanceledMailTemplate != null)
                repo.includes.Add("BookingCanceledMailTemplate");
            if (dto.DefaultMailConfig != null)
                repo.includes.Add("DefaultMailConfig");
        }

        protected override HomeConfig DoPostPutDto(Client currentClient, HomeConfigDTO dto, HomeConfig entity, string path, object param)
        {
            if (entity == null)
                entity = new HomeConfig();
            else
            {
                if (dto.EnableDinner == null)
                    dto.EnableDinner = entity.EnableDinner;
                if (dto.HourFormat24 == null)
                    dto.HourFormat24 = entity.HourFormat24;
            }
            GetMapper.Map(dto, entity);
            if (dto.BookingCanceledMailTemplate != null)
                entity.BookingCanceledMailTemplate = DocumentService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.BookingCanceledMailTemplate, currentClient, path);
            if (dto.DefaultMailConfig != null)
                entity.DefaultMailConfig = MailConfigService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.DefaultMailConfig, currentClient, path);
            return entity;
        }

        protected override void SetDefaultValues(HomeConfig entity)
        {
            entity.EnableDinner = entity.EnableDinner ?? true;
            entity.Devise = entity.Devise ?? BASIC_DEVISE;
            entity.HourFormat24 = entity.HourFormat24 ?? true;
        }
    }
}