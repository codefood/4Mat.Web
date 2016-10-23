using _4Mat.Processing.Messages.Validators;
using _4Mat.Tests.Init;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Tests
{
    [TestClass]
    public class DocumentMessageValidatorTests
    {

        [TestMethod]
        public void ValidateMessage_NoApiKey()
        {
            var validator = new DocumentMessageValidator();
            var message = (new DocumentMessageBuilder())
                .WithEmptyApiKey()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFile()
                .WithValidFileName()
                .Build();
            Assert.ThrowsException<ArgumentException>(() => validator.Validate(message));
        }
        [TestMethod]
        public void ValidateMessage_Valid()
        {
            var validator = new DocumentMessageValidator();
            var message = (new DocumentMessageBuilder())
                .WithValidApiKey()
                .WithCandidateId()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFile()
                .WithValidFileName()
                .Build();
            validator.Validate(message);
        }

        [TestMethod]
        public void ValidateMessage_NoDocument()
        {
            var validator = new DocumentMessageValidator();
            var message = (new DocumentMessageBuilder())
                .WithValidApiKey()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFileName()
                .Build();
            Assert.ThrowsException<ArgumentException>(() => validator.Validate(message));
        }

        [TestMethod]
        public void ValidateMessage_NoFileName()
        {
            var validator = new DocumentMessageValidator();
            var message = (new DocumentMessageBuilder())
                .WithValidApiKey()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFile()
                .Build();
            Assert.ThrowsException<ArgumentException>(() => validator.Validate(message));
        }

        [TestMethod]
        public void ValidateMessage_InvalidFileName()
        {
            var validator = new DocumentMessageValidator();
            var message = (new DocumentMessageBuilder())
                .WithValidApiKey()
                .WithDocumentType(Processing.DocumentType.CoverNote)
                .WithValidFile()
                .WithInvalidFileName()
                .Build();
            Assert.ThrowsException<ArgumentException>(() => validator.Validate(message));
        }
    }
}
