﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MiniORM;

#nullable disable

namespace MiniORM.Migrations
{
    [DbContext(typeof(TrainingDbContext))]
    partial class TrainingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MiniORM.Address", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);
                });

            modelBuilder.Entity("MiniORM.AdmissionTest", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("TestFees")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("AdmissionTest", (string)null);
                });

            modelBuilder.Entity("MiniORM.Course", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<double>("Fees")
                        .HasColumnType("float");

                    b.Property<int>("TeacherId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Course", (string)null);
                });

            modelBuilder.Entity("MiniORM.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PermanentAddressId")
                        .HasColumnType("int");

                    b.Property<int>("PresentAddressId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermanentAddressId");

                    b.HasIndex("PresentAddressId");

                    b.ToTable("Instructor", (string)null);
                });

            modelBuilder.Entity("MiniORM.Phone", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Phone", (string)null);
                });

            modelBuilder.Entity("MiniORM.Session", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("DurationInHour")
                        .HasColumnType("int");

                    b.Property<string>("LearningObjective")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TopicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.ToTable("Session", (string)null);
                });

            modelBuilder.Entity("MiniORM.Topic", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.ToTable("Topic", (string)null);
                });

            modelBuilder.Entity("MiniORM.AdmissionTest", b =>
                {
                    b.HasOne("MiniORM.Course", null)
                        .WithMany("Tests")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("MiniORM.Course", b =>
                {
                    b.HasOne("MiniORM.Instructor", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MiniORM.Instructor", b =>
                {
                    b.HasOne("MiniORM.Address", "PermanentAddress")
                        .WithMany()
                        .HasForeignKey("PermanentAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MiniORM.Address", "PresentAddress")
                        .WithMany()
                        .HasForeignKey("PresentAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PermanentAddress");

                    b.Navigation("PresentAddress");
                });

            modelBuilder.Entity("MiniORM.Phone", b =>
                {
                    b.HasOne("MiniORM.Instructor", null)
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("InstructorId");
                });

            modelBuilder.Entity("MiniORM.Session", b =>
                {
                    b.HasOne("MiniORM.Topic", null)
                        .WithMany("Sessions")
                        .HasForeignKey("TopicId");
                });

            modelBuilder.Entity("MiniORM.Topic", b =>
                {
                    b.HasOne("MiniORM.Course", null)
                        .WithMany("Topics")
                        .HasForeignKey("CourseId");
                });

            modelBuilder.Entity("MiniORM.Course", b =>
                {
                    b.Navigation("Tests");

                    b.Navigation("Topics");
                });

            modelBuilder.Entity("MiniORM.Instructor", b =>
                {
                    b.Navigation("PhoneNumbers");
                });

            modelBuilder.Entity("MiniORM.Topic", b =>
                {
                    b.Navigation("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}