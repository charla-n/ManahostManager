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
    public class MailConfigService : AbstractService<MailConfig, MailConfigDTO>
    {
        private new IMailConfigRepository repo;

        public MailConfigService(IMailConfigRepository repo, IValidation<MailConfig> validation)
            : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(MailConfigDTO dto, int id, Client currentClient)
        {
            orig = repo.GetMailConfigById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override MailConfig DoPostPutDto(Client currentClient, MailConfigDTO dto, MailConfig entity, string path, object param)
        {
            if (entity == null)
                entity = new MailConfig();
            else
            {
                if (dto.IsSSL == null)
                    dto.IsSSL = entity.IsSSL;
            }
            GetMapper.Map(dto, entity);
            return entity;
        }

        protected override void SetDefaultValues(MailConfig entity)
        {
            entity.IsSSL = entity.IsSSL ?? true;
        }
    }
}