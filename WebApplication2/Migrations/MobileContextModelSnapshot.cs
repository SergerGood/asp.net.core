using System;
using ASP.NET.Sample.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ASP.NET.Sample.Web.Migrations
{
    [DbContext(typeof(MobileContext))]
    internal partial class MobileContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication2.Models.Order",
                b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("ContactPhone");

                    b.Property<int>("PhoneId");

                    b.Property<string>("User");

                    b.HasKey("OrderId");

                    b.HasIndex("PhoneId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebApplication2.Models.Phone",
                b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Company");

                    b.Property<string>("Name");

                    b.Property<int>("Price");

                    b.HasKey("Id");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("WebApplication2.Models.Order",
                b =>
                {
                    b.HasOne("WebApplication2.Models.Phone", "Phone")
                        .WithMany()
                        .HasForeignKey("PhoneId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
