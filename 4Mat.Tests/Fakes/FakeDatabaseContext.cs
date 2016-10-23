using _4Mat.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Tests.Fakes
{
    public class FakeDatabaseContext : IDatabaseContext
    {
        public FakeDatabaseContext()
        {
            Documents = new List<Document>();
        }

        public IEnumerable<Document> Documents { get; set; }

        public void AddDocument(Document document)
        {
            ((List<Document>)Documents).Add(document);
        }

        public int SaveChanges()
        {
            return 1;
        }
    }
}
