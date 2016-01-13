using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public class PeopleFieldValidation : AbstractValidation<PeopleField>
    {
        [Dependency]
        public IFieldGroupRepository FieldGroupRepository { get; set; }
        [Dependency]
        public IPeopleRepository PeopleRepository { get; set; }

        public bool PostFieldValidation(ModelStateDictionary validationDictionary, Client currentClient, int FieldGroupId, int PeopleId)
        {
            if ((FieldGroupRepository.GetFieldGroupById(FieldGroupId, currentClient.Id) == null))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "FieldGroupId"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            if ((PeopleRepository.GetPeopleById(PeopleId, currentClient.Id)) == null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleField>(), "PeopleId"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            return validationDictionary.IsValid;
        }
    }
}