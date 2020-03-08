using DataLayer.DataClasses;
using Microsoft.EntityFrameworkCore;
using StudentApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudApp.Models
{
    public class StudentContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Person> Persons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=localappdb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Student>()
                        .HasIndex(u => u.Identifier)
                        .IsUnique();
            modelBuilder.Entity<StudentGroup>()
                .HasKey(sg => new { sg.StudentId, sg.GroupId });

            modelBuilder.Entity<StudentGroup>().HasOne(sg => sg.Student)
                .WithMany(p => p.StudentGroup)
                .HasForeignKey(pe => pe.StudentId);
            modelBuilder.Entity<StudentGroup>().HasOne(sg => sg.Group)
               .WithMany(g => g.StudentGroup)
               .HasForeignKey(pe => pe.GroupId);
            modelBuilder.Entity<Gender>().HasData(
                new Gender { Id = 1, Name = "male" },
                new Gender { Id = 2, Name = "female" },
                new Gender { Id = 3, Name = "none" });
            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, Login = "admin@gmail.com", Password = "12345", Role = "admin" },
                new Person { Id = 2, Login = "user@gmail.com", Password = "55555", Role = "user" });
            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, FirstName = "Egor", LastName = "Starchikov", Patronymic = "Alexandrovich", GenderId = 1, Identifier = "47" },
                new Student { Id = 2, FirstName = "Ivan", LastName = "Ivanov", Patronymic = "Ivanovich", GenderId = 2, Identifier = "11" }
                );
            modelBuilder.Entity<Group>().HasData(
                new Group { Id = 1, Name = "FirstGroup" },
                new Group { Id = 2, Name = "SecondGroup" }
            );
            modelBuilder.Entity<StudentGroup>().HasData(
                new StudentGroup { StudentId = 1, GroupId = 1},
                new StudentGroup { StudentId = 1, GroupId = 2},
                new StudentGroup { StudentId = 2, GroupId = 2}
                );
        }
    }
}
