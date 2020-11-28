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
            try
            {
                IEnumerable<Person> personList = GetAllPeople();
                return View(personList);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Exception", ex);
            }
        }

        public IEnumerable<Person> GetAllPeople()
        {
            return _db.Person;
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
            try
            {
                int rowsAffected = SavePerson(person);
                if (rowsAffected < 1)
                {
                    // return redirect to view for a failed save action
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Exception", ex);
            }
        }

        public int SavePerson(Person person)
        {
            _db.Add(person);
           return _db.SaveChanges();
        }
        // Get Person to Edit
        public IActionResult EditPerson(int? id)
        {
            try
            {
                if (id is < 1)
                {
                    return NotFound();
                }
                var person = FindPerson(id);
                if (person is null)
                {
                    return NotFound();
                }
                return View(person);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Exception", ex);
            }
        }

        public Person FindPerson(int? id)
        {
            return _db.Person.Find(id);
        }

        // Get Person to Delete
        public IActionResult DeletePerson(int? id)
        {
            try
            {
                if (id is < 1)
                {
                    return NotFound();
                }
                var person = FindPerson(id);
                if (person is null)
                {
                    return NotFound();
                }
                return View(person);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Exception", ex);
            }
        }

        // Delete Person
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePersonPost(int? id)
        {
            try
            {
                var person = FindPerson(id);
                if (person is null)
                {
                    return NotFound();
                }
                int rowsAffected = RemovePerson(person);
                if (rowsAffected < 1)
                {
                    // // return redirect to view for a failed delete action
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Exception", ex);
            }
        }

        public int RemovePerson(Person person)
        {
            _db.Remove(person);
            return _db.SaveChanges();
        }

        // Update Person Details
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPerson(Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int rowsAffected = UpdatePerson(person);
                    if (rowsAffected < 1)
                    {
                        // // return redirect to view for a failed delete action
                    }
                    return RedirectToAction("Index");
                }
                return View(person);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Exception",ex);
            }

        }

        public int UpdatePerson(Person person)
        {
            _db.Update(person);
            return _db.SaveChanges();
        }
    }
}
