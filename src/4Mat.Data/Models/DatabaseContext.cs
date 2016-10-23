using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _4Mat.Data.Models
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=FOURMat;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
       
        public DbSet<Document> Documents { get; set; }

        IEnumerable<Document> IDatabaseContext.Documents
        {
            get
            {
                return Documents;
            }
        }

        public void AddDocument(Document document)
        {
            Documents.Add(document);
            SaveChanges();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
