using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Web.Models
{
    public class DocumentUploadDetails
    {
        
        public string CandidateId { get; set; }
        
        public string EmployerId { get; set; }

        public string FileName { get; set; }

        public string DocumentType { get; set; }
    }
}
