﻿// <auto-generated />
using System;
using BibleBlast.API.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BibleBlast.API.Migrations
{
    [DbContext(typeof(SqlServerAppContext))]
    [Migration("20190228023641_AddKidEntity")]
    partial class AddKidEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BibleBlast.API.Models.Kid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Birthday");

                    b.Property<DateTime>("DateRegistered");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("Grade");

                    b.Property<bool>("IsActive");

                    b.Property<string>("LastName");

                    b.HasKey("Id");

                    b.ToTable("Kid");
                });
#pragma warning restore 612, 618
        }
    }
}
