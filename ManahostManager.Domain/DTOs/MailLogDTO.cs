using System;

namespace ManahostManager.Domain.DTOs
{
    public class MailLogDTO : IDTO
    {
        public int? Id { get; set; }

        public bool? Successful { get; set; }

        public string Reason { get; set; }

        public DateTime? DateModification { get; set; }

        public string To { get; set; }

        public DateTime? DateSended { get; set; }

        public int? HomeId { get; set; }

        int IDTO.HomeId
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