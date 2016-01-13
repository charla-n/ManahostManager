using MsgPack.Serialization;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ManahostManager.Domain.Entity
{
    // Document of any types

    [Table("Document")]
    public class Document : IEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? DateUpload { get; set; }

        public long SizeDocument { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public DocumentCategory DocumentCategory { get; set; }

        public Boolean IsPrivate { get; set; }

        public String Title { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public String Url { get; set; }

        [IgnoreDataMember]
        [MessagePackIgnore]
        public Home Home { get; set; }

        public int HomeId { get; set; }

        public Boolean Hide { get; set; }

        // Type of the document filled by the API
        public String MimeType { get; set; }

        public DateTime? DateModification { get; set; }

        public int? GetFK()
        {
            return HomeId;
        }

        public void SetDateModification(DateTime? d)
        {
            DateModification = d;
        }
    }
}