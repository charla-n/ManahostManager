using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;

namespace ManahostManager.Domain.DTOs
{
    public class PhoneNumberDTO : IDTO
    {
        public int? Id { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public PhoneType PhoneType { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Phone { get; set; }

        public int HomeId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}