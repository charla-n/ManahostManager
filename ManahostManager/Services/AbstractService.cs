using ManahostManager.App_Start;
using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.ManahostPatcher;
using ManahostManager.Utils.Mapper;
using ManahostManager.Validation;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public interface IAbstractService<ENTITY, DTO>
    {
        DTO PrePostDTO(Client currentClient, DTO dto, Object param);

        DTO PrePostDTO(ModelStateDictionary modelState, Client currentClient, DTO dto);

        void PrePutDTO(Client currentClient, int id, DTO dto, Object param);

        void PrePutDTO(ModelStateDictionary modelState, Client currentClient, int id, DTO dto);

        ENTITY PrePostDTOToEntity(Client currentClient, DTO dto, string path, Object param);

        ENTITY PrePost(Client currentClient, ENTITY entity, Object param);

        void PreDelete(Client currentClient, int id, Object param);

        void PreDelete(ModelStateDictionary modelState, Client currentClient, int id);

        void PrePut(Client currentClient, ENTITY entity, Object param);

        IEnumerable<DTO> DoGet(Client currentClient, Object param);

        DTO DoGet(Client currentClient, int id, Object param);

        void PreDeleteManual(Client currentClient, int id, Object param);

        ENTITY PreProcessDTOPostPut(int homeId, DTO dto, Client currentClient, string path, object param);

        ENTITY PreProcessDTOPostPut(ModelStateDictionary modelState, int homeId, DTO dto, Client currentClient, string path);

        void SetModelState(ModelStateDictionary Model);

        IAbstractRepository<ENTITY> GetRepo();
    }

    //TODO: DTO DoPostAfterSave/DoPutAfterSave won't work for nested dtos/entities
    public abstract class AbstractService<ENTITY, DTO> : IAbstractService<ENTITY, DTO> where ENTITY : class
                                                                                       where DTO : class
    {
        protected ModelStateDictionary validationDictionnary;
        protected IValidation<ENTITY> validation;
        protected IAbstractRepository<ENTITY> repo;
        protected ENTITY orig;
        protected string nestedPath;

        [Dependency]
        public UnityRegisterScope DependencyScope { get; set; }

        public IMapper GetMapper { get { return GetService<IMapper>(); } private set { } }

        public U GetService<U>() where U : class
        {
            return DependencyScope.Scope.GetService(typeof(U)) as U;
        }

        public AbstractService(ModelStateDictionary dict, IAbstractRepository<ENTITY> repo, IValidation<ENTITY> validation)
        {
            this.validation = validation;
            this.repo = repo;
            this.validationDictionnary = dict;
        }

        public AbstractService(ModelStateDictionary dict, IValidation<ENTITY> validation) : this(dict, null, validation)
        { }

        public AbstractService(ModelStateDictionary dict) : this(dict, null, null)
        { }

        public AbstractService(IAbstractRepository<ENTITY> repo, IValidation<ENTITY> validation) : this(null, repo, validation)
        { }

        public AbstractService(IValidation<ENTITY> validation, IAbstractRepository<ENTITY> repo) : this (null, repo, validation)
        { }

        public AbstractService()
        { }

        public void PreDeleteManual(Client currentClient, int id, Object param)
        {
            DoDelete(currentClient, id, param);
        }

        public void SetModelState(ModelStateDictionary Model)
        {
            this.validationDictionnary = Model;
        }

        private bool ValidateModel(DTO dto)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            var ret = Validator.TryValidateObject(dto, new ValidationContext(dto), results, true);

            results.ForEach(result =>
            {
                foreach (string membername in result.MemberNames)
                {
                    validationDictionnary.AddModelError(membername, result.ErrorMessage);
                }
            });
            return ret;
        }

        public DTO PrePostDTO(Client currentClient, DTO dto, Object param)
        {
            ValidateNull<DTO>(dto);
            ((IDTO)dto).Id = 0;
            if (!ValidateModel(dto))
                throw new ManahostValidationException(validationDictionnary);
            orig = DoPostPutDto(currentClient, dto, null, dto.GetType().Name, param);
            ((IEntity)orig).SetDateModification(null);
            SetDefaultValues(orig);
            if (!validation.PreValidatePost(validationDictionnary, currentClient, orig, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoPost(currentClient, orig, param);
            repo.Add(orig);
            if (!repo.Save())
            {
                validationDictionnary.AddModelError("DB", GenericError.SQLEXCEPTION);
                throw new ManahostValidationException(validationDictionnary);
            }
            DoPostAfterSave(currentClient, orig, param);
            return GetMapper.Map<ENTITY, DTO>(orig);
        }

        public DTO PrePostDTO(ModelStateDictionary modelState, Client currentClient, DTO dto)
        {
            this.validationDictionnary = modelState;
            return PrePostDTO(currentClient, dto, null);
        }

        public void PrePutDTO(Client currentClient, int id, DTO dto, Object param)
        {
            ValidateNull<DTO>(dto);
            ((IDTO)dto).Id = 0;
            if (!ValidateModel(dto))
                throw new ManahostValidationException(validationDictionnary);
            ((IDTO)dto).Id = id;
            PutIncludeProps(dto);
            ProcessDTOPostPut(dto, 0, currentClient);
            ValidateOrig();
            orig = DoPostPutDto(currentClient, dto, orig, dto.GetType().Name, param);
            ((IEntity)orig).SetDateModification(DateTime.UtcNow);
            if (!validation.PreValidatePut(validationDictionnary, currentClient, orig, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoPut(currentClient, orig, param);
            repo.Update(orig);
            if (!repo.Save())
            {
                validationDictionnary.AddModelError("DB", GenericError.SQLEXCEPTION);
                throw new ManahostValidationException(validationDictionnary);
            }
            DoPutAfterSave(currentClient, orig, param);
        }

        public void PrePutDTO(ModelStateDictionary modelState, Client currentClient, int id, DTO dto)
        {
            this.validationDictionnary = modelState;
            PrePutDTO(currentClient, id, dto, null);
        }

        public ENTITY PrePostDTOToEntity(Client currentClient, DTO dto, string path, Object param)
        {
            ValidateNull<DTO>(dto);
            orig = DoPostPutDto(currentClient, dto, null, path, param);
            ((IEntity)orig).SetDateModification(null);
            SetDefaultValues(orig);
            if (!validation.PreValidatePost(validationDictionnary, currentClient, orig, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoPost(currentClient, orig, param);
            return orig;
        }

        public ENTITY PrePutDTOToEntity(Client currentClient, DTO dto, ENTITY entity, string path, Object param)
        {
            ValidateNull<DTO>(dto);
            orig = DoPostPutDto(currentClient, dto, entity, path, param);
            ((IEntity)orig).SetDateModification(DateTime.UtcNow);
            PutIncludeProps(dto);
            if (!validation.PreValidatePut(validationDictionnary, currentClient, orig, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoPut(currentClient, orig, param);
            return orig;
        }

        // TODO : to remove once document has been migrated to dto
        public ENTITY PrePost(Client currentClient, ENTITY entity, Object param)
        {
            ValidateNull(entity);
            ((IEntity)entity).SetDateModification(null);
            SetDefaultValues(entity);
            if (!validation.PreValidatePost(validationDictionnary, currentClient, entity, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoPost(currentClient, entity, param);
            repo.Add(entity);
            repo.Save();
            DoPostAfterSave(currentClient, entity, param);
            return entity;
        }

        // TODO : to remove once document has been migrated to dto
        public void PrePut(Client currentClient, ENTITY entity, Object param)
        {
            ValidateNull(entity);
            ((IEntity)entity).SetDateModification(DateTime.UtcNow);
            PutIncludeProps(null);
            ProcessDTOPostPut(GetMapper.Map<ENTITY, DTO>(entity), 0, currentClient);
            ValidateOrig();
            PatchObjectUtils<ENTITY>.ReplacementOrigByGiven(entity, orig, new string[] { });
            if (!validation.PreValidatePut(validationDictionnary, currentClient, orig, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoPut(currentClient, orig, param);
            repo.Update(orig);
            repo.Save();
            DoPutAfterSave(currentClient, orig, param);
        }

        public void PreDelete(Client currentClient, int id, Object param)
        {
            DeleteIncludeProps();
            ProcessDTOPostPut(null, id, currentClient);
            ValidateOrig();
            if (!validation.PreValidateDelete(validationDictionnary, currentClient, orig, param, repo))
                throw new ManahostValidationException(validationDictionnary);
            DoDelete(currentClient, id, param);
            repo.Delete(orig);
            repo.Save();
        }

        public void PreDelete(ModelStateDictionary modelState, Client currentClient, int id)
        {
            this.validationDictionnary = modelState;
            PreDelete(currentClient, id, null);
        }

        public ENTITY PreProcessDTOPostPut(int homeId, DTO dto, Client currentClient, string path, object param)
        {
            path = string.Join(".", path, dto.GetType().Name);
            if (!GenericNames.ALLOWED_DTO_PATH.Contains(path))
                return null;
            ((IDTO)dto).HomeId = homeId;
            if (((IDTO)dto).Id == null)
                return null;
            else if (((IDTO)dto).Id == 0)
                return PrePostDTOToEntity(currentClient, dto, path, param);
            else
            {
                ProcessDTOPostPut(dto, 0, currentClient);
                if (orig == null)
                    validationDictionnary.AddModelError(TypeOfName.GetNameFromType<ENTITY>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                return orig;
            }
        }

        public ENTITY PreProcessDTOPostPut(ModelStateDictionary model, int homeId, DTO dto, Client currentClient, string path)
        {
            this.validationDictionnary = model;
            return PreProcessDTOPostPut(homeId, dto, currentClient, path, null);
        }

        protected virtual void SetDefaultValues(ENTITY entity)
        {
        }

        protected virtual void PutIncludeProps(DTO dto)
        {
        }

        protected virtual void DeleteIncludeProps()
        {
        }

        protected virtual ENTITY DoPostPutDto(Client currentClient, DTO dto, ENTITY entity, string path, object param)
        {
            return null;
        }

        public virtual void ProcessDTOPostPut(DTO dto, int id, Client currentClient)
        {
        }

        protected virtual void DoPostAfterSave(Client currentClient, ENTITY entity, object param)
        {
        }

        protected virtual void DoPutAfterSave(Client currentClient, ENTITY entity, object param)
        {
        }

        protected virtual void DoPost(Client currentClient, ENTITY entity, object param)
        {
        }

        protected virtual void DoPut(Client currentClient, ENTITY entity, object param)
        {
        }

        protected virtual void DoDelete(Client currentClient, int id, object param)
        {
        }

        public virtual IEnumerable<DTO> DoGet(Client currentClient, Object param)
        {
            throw new NotImplementedException();
        }

        public virtual DTO DoGet(Client currentClient, int id, Object param)
        {
            throw new NotImplementedException();
        }

        protected void ValidateNull<U>(U entity) where U : class
        {
            if (entity == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<U>(), GenericError.CANNOT_BE_NULL_OR_EMPTY);
                throw new ManahostValidationException(validationDictionnary);
            }
        }

        protected void ValidateOrig()
        {
            if (orig == null)
            {
                validationDictionnary.AddModelError(TypeOfName.GetNameFromType<DTO>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                throw new ManahostValidationException(validationDictionnary);
            }
        }

        public IAbstractRepository<ENTITY> GetRepo()
        {
            return repo;
        }
    }
}