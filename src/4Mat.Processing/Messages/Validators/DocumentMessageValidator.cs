using _4Mat.Data;
using _4Mat.Data.External;
using _4Mat.Processing.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Processing.Messages.Validators
{
    public class DocumentMessageValidator : IMessageValidator
    {
        public DocumentMessageValidator()
        {
            ValidationRules = new List<Rule>
            {
                new Rule("ApiKey"),
                new Rule("CandidateId"),
                new Rule("DocumentFileName")
            };
        }
        
        public IList<Rule> ValidationRules { get; private set; }

        public void UpdateModule(Data.External.Module module)
        {
            ValidationRules.Add(new Rule("EmployerId", module.IsActive));
        }

        //TODO: Make these methods private and use [InternalsVisibleTo] in assemblyinfo
        public void Validate(MessageBase message)
        {
            if (message == null)
                throw new ArgumentNullException("mesasage");
            var documentMessage = message as DocumentMessage;
            if (documentMessage == null)
                throw new ArgumentException("Message is not a DocumentMessage");

            var properties = documentMessage.GetType().GetProperties();
            foreach(var property in properties)
            {
                var rules = ValidationRules.Where(x => x.FieldName.Equals(property.Name, StringComparison.OrdinalIgnoreCase));
                foreach(var rule in rules)
                {
                    var value = property.GetValue(documentMessage);
                    if (rule.Required && 
                        (value == null || 
                        value.Equals(GetDefaultValue(value.GetType())) ||
                        string.IsNullOrEmpty(value.ToString())))

                        throw new ArgumentException(string.Format("{0} is not set", rule.FieldName));
                }
            }
        }

        private object GetDefaultValue(Type t)
        {
            if (t.GetTypeInfo().IsValueType)
            {
                return Activator.CreateInstance(t);
            }
            else
            {
                return null;
            }
        }

    }
}
