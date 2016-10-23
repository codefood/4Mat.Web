using _4Mat.Data.Models;
using _4Mat.Processing.Interfaces;
using _4Mat.Processing.Messages.Validators;
using _4Mat.Processing.Processors;
using _4Mat.Tests.Fakes;
using _4Mat.Tests.Init;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace _4Mat.Tests
{
    [TestClass]
    public class DocumentProcessorTests
    {
        public IModuleService FakeModuleService { get; set; }
        public IMessageValidator FakeValidator { get; set; }
        public IDatabaseContext FakeDatabaseContext { get; set; }

        [TestInitialize]
        public void CreateServices()
        {
            FakeModuleService = new FakeModuleService();
            FakeValidator = new FakeValidator();
            FakeDatabaseContext = new FakeDatabaseContext();
        }


        [TestMethod]
        public void CreateModelFromMessage_Valid()
        {
            //Invalid paths through this method are handled by ValidateMessage()
            var processor = new DocumentProcessor(FakeModuleService, FakeValidator, FakeDatabaseContext);
            var message = (new DocumentMessageBuilder())
                .WithValidApiKey()
                .WithCandidateId()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFile()
                .WithValidFileName()
                .Build();
            var model = processor.CreateModelFromMessage(message);

            Assert.IsNotNull(model);
            Assert.AreEqual(message.CandidateId, model.CandidateId);
            Assert.AreEqual(message.CreatedDate, model.CreationDate);
            Assert.AreEqual(message.DocumentFileName, model.DocumentFileName);
            Assert.AreEqual((int)message.DocumentType, (int)model.DocumentType);
        }

        [TestMethod]
        public void ProcessMessage_CommitsToDatabase()
        {
            //Invalid paths through this method are handled by ValidateMessage()
            var processor = new DocumentProcessor(FakeModuleService, FakeValidator, FakeDatabaseContext);
            var message = (new DocumentMessageBuilder())
                .WithValidApiKey()
                .WithCandidateId()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFile()
                .WithValidFileName()
                .Build();

            var originalCount = ((List<Document>)FakeDatabaseContext.Documents).Count;

            processor.ProcessMessage(message);

            Assert.AreEqual(((List<Document>)FakeDatabaseContext.Documents).Count, originalCount + 1);
        }

    }
}