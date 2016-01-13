using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public class KeyGeneratorService : AbstractService<KeyGenerator, KeyGeneratorDTO>
    {
        private new IKeyGeneratorRepository repo;

        public KeyGeneratorService(IKeyGeneratorRepository repo, IValidation<KeyGenerator> validation) : base(validation, repo)
        {
            this.repo = repo;
        }

        public override void ProcessDTOPostPut(KeyGeneratorDTO dto, int id, Client currentClient)
        {
            orig = repo.GetKeyGeneratorByClientId(dto == null ? id : (int)dto.Id, currentClient.Id);
        }

        protected override KeyGenerator DoPostPutDto(Client currentClient, KeyGeneratorDTO dto, KeyGenerator entity, string path, object param)
        {
            if (entity == null)
                entity = new KeyGenerator();
            else
            {
                if (entity.HomeId == null)
                    dto.HomeId = entity.HomeId;
            }
            GetMapper.Map(dto, entity);
            return entity;
        }

        protected override void SetDefaultValues(KeyGenerator entity)
        {
            entity.KeyType = EKeyType.CLIENT;
            entity.Key = KeyGeneratorUtils.GenerateKey();
        }
    }
}