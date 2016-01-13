using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Validation;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class SatisfactionConfigService : AbstractService<SatisfactionConfig, SatisfactionConfigDTO>
    {
        public SatisfactionConfigService(ISatisfactionConfigRepository repo, IValidation<SatisfactionConfig> validation)
            : base(validation, repo)
        { }
    }
}