﻿using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Document of any types

    public class DocumentDTO : IDTO
    {
        public int? Id { get; set; }

        // Set by the API at utcnow
        public DateTime? DateUpload { get; set; }

        // Set by the API, initialized at 0 before the upload
        // Size of the document in bytes
        public long? SizeDocument { get; set; }

        // Optional
        // A document does not have necessarily a category
        public DocumentCategoryDTO DocumentCategory { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Boolean IsPrivate { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Generated by the API, can't be modified by the manager
        [MaxLength(2048, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        public Boolean Hide { get; set; }

        // Type of the document filled by the API
        public String MimeType { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}