using _4Mat.Processing;
using _4Mat.Processing.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Tests.Init
{
    public class DocumentMessageBuilder
    {
        public DocumentMessageBuilder()
        {
            DocumentMessage = new DocumentMessage();
        }

        private DocumentMessage DocumentMessage { get; set; }

        public DocumentMessage Build()
        {
            return DocumentMessage;
        }

        public DocumentMessageBuilder WithValidApiKey()
        {
            DocumentMessage.ApiKey = Guid.Parse("DC677876-062A-4FC6-AD1D-7494B70FE8C1"); //6A9393B4-DBF1-4628-BD69-410D5DD4BF82
            return this;
        }

        public DocumentMessageBuilder WithCandidateId()
        {
            DocumentMessage.CandidateId = Guid.NewGuid();
            return this;
        }

        public DocumentMessageBuilder WithEmptyApiKey()
        {
            DocumentMessage.ApiKey = Guid.Empty;
            return this;
        }

        public DocumentMessageBuilder WithValidFile()
        {
            using (var br = new BinaryReader(File.OpenRead("Init\\LA-CV.docx")))
            {
                DocumentMessage.Document = br.ReadBytes((int)br.BaseStream.Length);
            }
            return this;
        }

        public DocumentMessageBuilder WithValidFileName()
        {
            DocumentMessage.DocumentFileName = "LA-CV.docx";
            return this;
        }

        public DocumentMessageBuilder WithInvalidFileName()
        {
            DocumentMessage.DocumentFileName = "";
            return this;
        }

        public DocumentMessageBuilder  WithDocumentType(DocumentType type)
        {
            DocumentMessage.DocumentType = type;
            return this;
        }

        public DocumentMessageBuilder WithValidEmployeeId()
        {

            DocumentMessage.EmployerId = Guid.NewGuid();
            return this;
        }

        public DocumentMessageBuilder WithNullEmployeeId()
        {
            DocumentMessage.EmployerId = null;
            return this;
        }

    }
}
