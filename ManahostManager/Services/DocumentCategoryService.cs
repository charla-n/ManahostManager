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
    public class DocumentCategoryService : AbstractService<DocumentCategory, DocumentCategoryDTO>
    {
        private new IDocumentCategoryRepository repo;

        public DocumentCategoryService(IDocumentCategoryRepository repo, IValidation<DocumentCategory> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(DocumentCategoryDTO dto, int id, Client currentClient)
        {
            orig = repo.GetDocumentCategoryById(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override DocumentCategory DoPostPutDto(Client currentClient, DocumentCategoryDTO dto, DocumentCategory entity, string path, object param)
        {
            if (entity == null)
                entity = new DocumentCategory();
            GetMapper.Map(dto, entity);
            return entity;
        }
    }
}