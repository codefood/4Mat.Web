using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Data.Models
{
    public interface IDatabaseContext
    {
        IEnumerable<Document> Documents { get; }
        int SaveChanges();
        void AddDocument(Document document);

    }
}
