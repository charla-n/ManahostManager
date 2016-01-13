using System;

namespace ManahostManager.Domain.DTOs
{
    public class ResourceConfigDTO : IDTO
    {
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

        public int? Id { get; set; }

        public long LimitBase { get; set; }
    }
}