using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _4Mat.Logging;
using _4Mat.Processing.Messages;
using _4Mat.Processing.Interfaces;
using _4Mat.Processing.Processors;
using _4Mat.Processing.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using _4Mat.Processing.Messages.Validators;
using _4Mat.Data;
using _4Mat.Data.Models;
using _4Mat.Web.Models;
using Microsoft.Extensions.Options;
using _4Mat.Processing;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace _4Mat.Web.Controllers
{
    [Route("api/[controller]")]
    public class DocumentController : Controller
    {

        public DocumentController(IOptions<AppSettings> settings)
        {
            //TODO: Use a DI container to resolve these at runtime
            var moduleService = new ModuleService();
            var messageValidator = new DocumentMessageValidator();
            DatabaseContext = new DatabaseContext();
            MessageProcesor = new DocumentProcessor(moduleService, messageValidator, DatabaseContext);
            
            Settings = settings;

        }

        private DatabaseContext DatabaseContext { get; set; }
        private IProcessor<DocumentMessage> MessageProcesor { get; set; }
        private IOptions<AppSettings> Settings { get; set; }

        private void LoadValidationRules()
        {
            try
            {
                var apiKey = Settings.Value.ApiKey;
                var module = MessageProcesor.ModuleService.LoadModule(apiKey, ModuleService.ModuleNames.Employer);

                MessageProcesor.MessageValidator.UpdateModule(module);
            }
            catch (Exception ex)
            {
                //Assume we can continue with no modules available
                Logger.Log(ex);
            }
        }

        // GET: api/Document/ValidationRules
        [HttpGet("ValidationRules")]
        public IEnumerable<Rule> ValidationRules()
        {
            Logger.Log(HttpCommand.GET, Request);

            LoadValidationRules();

            return MessageProcesor.MessageValidator.ValidationRules;
        }

        // GET api/values/{guid}
        [HttpGet("{id}")]
        public IEnumerable<Document> Get(Guid id)
        {
            Logger.Log(HttpCommand.GET, Request);

            return DatabaseContext.Documents.Where(x => x.CandidateId.Equals(id));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]DocumentUploadDetails model)
        {
            Logger.Log(HttpCommand.POST, Request);
            
            try
            {
                LoadValidationRules();

                var message = CreateMessageFromModel(model);

                MessageProcesor.ProcessMessage(message);
            }
            catch (ValidationException)
            {
                //Can be thrown back to the client
                throw;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                //An internal error has been thrown, so we don't want to expose our internals to the public
                throw new Exception("There was an error uploading your document");

            }

        }

        private DocumentMessage CreateMessageFromModel(DocumentUploadDetails model)
        {
            if (model == null)
                throw new ValidationException("No data received");

            Guid candidateGuid = Guid.Empty;

            if (string.IsNullOrEmpty(model.CandidateId) || !Guid.TryParse(model.CandidateId, out candidateGuid))
                throw new ValidationException("No or invalid CandidateId specified");

            Guid employerGuid = Guid.Empty;

            if (string.IsNullOrEmpty(model.EmployerId) || !Guid.TryParse(model.EmployerId, out employerGuid))
                throw new ValidationException("No or invalid EmployerId specified");

            int docTypeInt;
            
            if (string.IsNullOrEmpty(model.DocumentType) || !int.TryParse(model.DocumentType, out docTypeInt) || !Enum.IsDefined(typeof(DocumentType), docTypeInt))
                throw new ValidationException("No or invalid DocumentType selected");

            DocumentType docType = (DocumentType)docTypeInt;

            return new DocumentMessage
            {
                ApiKey = Settings.Value.ApiKey,
                CandidateId = candidateGuid,
                EmployerId = employerGuid,
                DocumentFileName = model.FileName,
                CreatedDate = DateTime.UtcNow,
                DocumentType = docType,
                Document = null
            };

        }

    }
}
