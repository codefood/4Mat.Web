using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using _4Mat.Data.Models;

namespace _4Mat.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20161023163843_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("_4Mat.Data.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CandidateId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("DocumentFileExtension")
                        .HasAnnotation("MaxLength", 5);

                    b.Property<string>("DocumentFileName")
                        .HasAnnotation("MaxLength", 200);

                    b.Property<int>("DocumentType");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });
        }
    }
}
