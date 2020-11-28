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
                .UseInMemoryDatabase(databaseName: "nmicrosinterview")
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
                Assert.Equal(3, people.Count());
            }
        }
    }
}
