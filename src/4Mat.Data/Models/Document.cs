using _4Mat.Processing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Data.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }

        [MaxLength(200)]
        public string DocumentFileName { get; set; }
        [MaxLength(5)]
        public string DocumentFileExtension { get; set; }

        public DateTime CreationDate { get; set; }

        public int DocumentType { get; set; }

    }
}
