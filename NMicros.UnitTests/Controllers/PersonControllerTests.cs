using AspCoreMVC.Controllers;
using AspCoreMVC.Data;
using AspCoreMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NMicros.UnitTests
{
    public class PersonControllerTests
    {

        [Fact]
        public void GetAllPeopleShouldWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewgetall")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.Person.Add(new Person { Id = 2, FName = "Fname2", LName = "Lname2" });
                context.Person.Add(new Person { Id = 3, FName = "Fname3", LName = "Lname3"});
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                IEnumerable<Person> people = personController.GetAllPeople();
                Assert.NotNull(people);
                Assert.Equal(3, people.Count());
            }
        }

        [Fact]
        public void GetAllPeopleShouldNotWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewgetallnotwork")
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                IEnumerable<Person> people = personController.GetAllPeople();
                Assert.NotNull(people);
                Assert.Empty(people);
            }
        }

        [Fact]
        public void SavePersonShouldWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewinsertvalid")
                .Options;

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                Person person = new Person { Id = 1, FName = "Fname1", LName = "Lame1", Status = "M" };
                PersonController personController = new PersonController(context);
                int rows = personController.SavePerson(person);
                Assert.Equal(1, rows);
            }
        }

        [Fact]
        public void SavePersonShouldNotWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewinsertinvalid")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                Person person = new Person { Id = 1, FName = "Fname1", LName = "Lame1", Status="M" };
                PersonController personController = new PersonController(context);
                Assert.Throws<System.ArgumentException>( () => personController.SavePerson(person));
            }
        }

        [Fact]
        public void FindPersonShoudWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewfindone")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                Person person = personController.FindPerson(1);
                Assert.NotNull(person);
                Assert.Equal(1, person.Id);
            }
        }

        [Fact]
        public void FindPersonShoudNotWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewfindonenotwork")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                Person person = personController.FindPerson(2);
                Assert.Null(person);
            }
        }

        [Fact]
        public void DeletePersonShoudWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewdelete")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.Person.Add(new Person { Id = 2, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                Person person = new Person { Id = 1, FName = "Fname1", LName = "Lame1" };
                int rows = personController.RemovePerson(person);

                Assert.Equal(1, rows);
            }
        }

        [Fact]
        public void DeletePersonShoudNotWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewdeletenotwork")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.Person.Add(new Person { Id = 2, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                Person person = new Person { Id = 3, FName = "Fname1", LName = "Lame1" };
                Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => personController.RemovePerson(person));
            }
        }

        [Fact]
        public void UpdatePersonShoudWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewupdate")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.Person.Add(new Person { Id = 2, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                Person person = new Person { Id = 1, FName = "Fnameee", LName = "Lameeee" };
                int rows = personController.UpdatePerson(person);

                Assert.Equal(1, rows);
            }
        }

        [Fact]
        public void UpdatePersonShoudNotWork()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "nmicrosinterviewupdatenotwork")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new AppDbContext(options))
            {
                context.Person.Add(new Person { Id = 1, FName = "Fname1", LName = "Lame1" });
                context.Person.Add(new Person { Id = 2, FName = "Fname1", LName = "Lame1" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new AppDbContext(options))
            {
                PersonController personController = new PersonController(context);
                Person person = new Person { Id = 3, FName = "Fnameee", LName = "Lamecc", Status="Divorced" };
                Assert.Throws<Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException>(() => personController.UpdatePerson(person));
            }
        }
    }
}
