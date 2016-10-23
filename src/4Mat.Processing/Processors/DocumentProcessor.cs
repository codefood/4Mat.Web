using _4Mat.Processing.Interfaces;
using _4Mat.Processing.Messages;
using _4Mat.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Processing.Processors
{
    public class DocumentProcessor : IProcessor<DocumentMessage>
    {

        public DocumentProcessor(IModuleService moduleService, 
            IMessageValidator messageValidator,
            IDatabaseContext databaseContext)
        {
            ModuleService = moduleService;
            MessageValidator = messageValidator;
            DatabaseContext = databaseContext;
        }

        public IModuleService ModuleService { get; set; }
        public IMessageValidator MessageValidator { get; set; }
        public IDatabaseContext DatabaseContext { get; set; }

        public void ProcessMessage(DocumentMessage message)
        {
            MessageValidator.Validate(message);

            var model = CreateModelFromMessage(message);

            DatabaseContext.AddDocument(model);
        }

        //TODO: Make these methods private and use [InternalsVisibleTo] in assemblyinfo
        public Document CreateModelFromMessage(DocumentMessage message)
        {
            MessageValidator.Validate(message);

            return new Document
            {
                CandidateId = message.CandidateId,
                CreationDate = message.CreatedDate,
                DocumentFileExtension = Path.GetExtension(message.DocumentFileName),
                DocumentFileName = message.DocumentFileName,
                DocumentType = (int)message.DocumentType
                //TODO: store the document itself? Document storage is not specified in the design
                //Document = message.Document
            };
        }


    }
}
