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
    public class SatisfactionConfigQuestionService : AbstractService<SatisfactionConfigQuestion, SatisfactionConfigQuestionDTO>
    {
        private new ISatisfactionConfigQuestionRepository repo;

        [Dependency]
        public IAbstractService<SatisfactionConfig, SatisfactionConfigDTO> SatisfactionConfigService { get { return GetService<IAbstractService<SatisfactionConfig, SatisfactionConfigDTO>>(); } private set { } }

        public SatisfactionConfigQuestionService(ISatisfactionConfigQuestionRepository repo, IValidation<SatisfactionConfigQuestion> validation) : base(repo, validation)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(SatisfactionConfigQuestionDTO dto, int id, Client currentClient)
        {
            orig = repo.GetSatisfactionConfigQuestionByHomeId(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override SatisfactionConfigQuestion DoPostPutDto(Client currentClient, SatisfactionConfigQuestionDTO dto, SatisfactionConfigQuestion entity, string path, object param)
        {
            if (entity == null)
                entity = new SatisfactionConfigQuestion();
            GetMapper.Map(dto, entity);
            if (dto.SatisfactionConfig != null)
                entity.SatisfactionConfig = SatisfactionConfigService.PreProcessDTOPostPut(validationDictionnary, dto.HomeId, dto.SatisfactionConfig, currentClient, path);
            return entity;
        }
    }
}