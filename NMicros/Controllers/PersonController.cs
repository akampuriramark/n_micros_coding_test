using AspCoreMVC.Data;
using AspCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly AppDbContext _db;
        public PersonController(AppDbContext db)
        {
            _db = db;
        }

        // View All People
        public IActionResult Index()
        {
            IEnumerable<Person> personList = _db.Person;
            return View(personList);
        }

        // Load Form to creat Person
        public IActionResult CreatePerson()
        {
            return View();
        }

        // Create Person 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePerson(Person person)
        {
            _db.Add(person);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        // Get Person to Edit
        public IActionResult EditPerson(int? id)
        { 
            if (id is < 1)
            {
                return NotFound();
            }
            var person = _db.Person.Find(id);
            if (person is null)
            {
                return NotFound(); 
            }
            return View(person);
        }

        // Get Person to Delete
        public IActionResult DeletePerson(int? id)
        {
            if (id is < 1)
            {
                return NotFound();
            }
            var person = _db.Person.Find(id);
            if (person is null)
            {
                return NotFound();
            }
            return View(person);
        }

        // Delete Person
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePersonPost(int? id)
        {
            var person = _db.Person.Find(id);
            if (person is null)
            {
                return NotFound();
            }
            _db.Remove(person);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Update Person Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                _db.Update(person);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }
    }
}
