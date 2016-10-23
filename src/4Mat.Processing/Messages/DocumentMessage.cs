using _4Mat.Processing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Processing.Messages
{
    public class DocumentMessage : MessageBase
    {
        public Guid CandidateId { get; set; }
        public string DocumentFileName { get; set; }
        public DocumentType DocumentType { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[] Document { get; set; }

        public Guid ApiKey { get; set; }
        public Guid? EmployerId { get; set; }
    }
}
