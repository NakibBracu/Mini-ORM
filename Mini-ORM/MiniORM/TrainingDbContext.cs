    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace MiniORM
    {
        public class TrainingDbContext : DbContext
        {
            //Keep ConnectionString

            private readonly string _connectionString;

            public TrainingDbContext()
            {
            _connectionString = "Server=.\\SQLEXPRESS; Database=MiniORM; User Id=aspnetb8;Password=123456;Trust Server Certificate=True; Network Library=TCP;";

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(_connectionString);
                    //Here Migration Assembly has been removed for simplicty but While we do Asp.Net web App, then we will add migrationAssembly after _connectionString in the above method.
                }
                base.OnConfiguring(optionsBuilder);
            }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<AdmissionTest> AdmissionTests { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<AdmissionTest>().ToTable("AdmissionTest");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<Phone>().ToTable("Phone");
            modelBuilder.Entity<Session>().ToTable("Session");
            modelBuilder.Entity<Topic>().ToTable("Topic");

            modelBuilder.Entity<Address>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<AdmissionTest>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Course>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Instructor>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Phone>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Session>().Property(x => x.Id).ValueGeneratedNever();
            modelBuilder.Entity<Topic>().Property(x => x.Id).ValueGeneratedNever();

           

        }
        





    }
}
