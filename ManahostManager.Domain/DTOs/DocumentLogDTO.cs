using System;

namespace ManahostManager.Domain.DTOs
{
    public class DocumentLogDTO
    {
        public int? Id { get; set; }

        public ClientDTO Client { get; set; }

        public long CurrentSize { get; set; }

        public long BuySize { get; set; }

        public ResourceConfigDTO ResourceConfig { get; set; }

        public DateTime? DateModification { get; set; }
    }
}