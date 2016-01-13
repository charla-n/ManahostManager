using ManahostManager.App_Start;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Services;
using ManahostManager.Utils;
using ManahostManager.Utils.Attributs;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ManahostManager.Controllers
{
    public class DocumentController : AbstractAPIController
    {
        [Dependency]
        public IAdditionnalDocumentMethod DocumentService { get; set; }
        [Dependency]
        public IHomeRepository HomeRepository { get; set; }
        [Dependency]
        public IDocumentLogRepository DocumentLogRepository { get; set; }
        [Dependency]
        public IDocumentRepository DocumentRepository { get; set; }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("api/Document")]
        [HttpPost]
        public Document Post([FromBody] Document entity)
        {
            return DocumentService.PrePost(currentClient, entity, null);
        }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpPut]
        [Route("api/Document")]
        public IHttpActionResult Put(Document d)
        {
            DocumentService.PrePut(currentClient, d, null);
            return Ok();
        }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("api/Document/{Id:int}")]
        [HttpPost]
        public HttpResponseMessage UploadDocument([FromUri] int Id)
        {
            Document entity = DocumentRepository.GetDocumentById(Id, currentClient.Id);

            return DocumentService.UploadFile(currentClient, entity, Request, HomeRepository, DocumentLogRepository);
        }

        [Route("api/Document/{Id:int}")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]int Id)
        {
            Document document = null;

            document = DocumentRepository.GetDocumentById(Id);
            return DocumentService.Get(document, Request, null);
        }

        [Route("api/Document/image/{Extension}/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetResizedFile([FromUri]String Extension, [FromUri]int Id)
        {
            Document document = null;

            document = DocumentRepository.GetDocumentById(Id);
            return DocumentService.Get(document, Request, null, Extension);
        }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("api/Document/{IdHome:int}/{Id:int}")]
        [HttpGet]
        public HttpResponseMessage GetPrivate([FromUri]int IdHome, [FromUri]int Id)
        {
            Document document = null;

            document = DocumentRepository.GetDocumentById(Id, currentClient.Id);
            return DocumentService.Get(document, Request, null, null, currentClient, IdHome);
        }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("api/Document/image/{Extension}/{IdHome}/{Id}")]
        [HttpGet]
        public HttpResponseMessage GetPrivateResizedFile([FromUri]String Extension, [FromUri]int IdHome, [FromUri]int Id)
        {
            Document document = null;

            document = DocumentRepository.GetDocumentById(Id, currentClient.Id);
            return DocumentService.Get(document, Request, null, Extension, currentClient, IdHome);
        }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [Route("api/Document/{Id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete([FromUri]int Id)
        {
            DocumentService.PreDelete(currentClient, Id, null);
            return Ok();
        }

        [ManahostAuthorize(Roles = GenericNames.MANAGER_REGISTERED_VIP)]
        [HttpGet]
        [Route("api/Document/Log")]
        public async Task<IHttpActionResult> Get()
        {
            DocumentLog docLog = await DocumentLogRepository.GetDocumentLogByIdAsync(currentClient.Id);
            return Ok(docLog);
        }
    }
}