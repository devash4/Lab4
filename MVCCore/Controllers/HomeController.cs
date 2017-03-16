using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCCore.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCCore.Controllers
{
    public class HomeController : Controller
    {
        private PersonRepository repo;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
            repo = new PersonRepository();
        }
    
        public IActionResult Index()
        {
            string welcomeMessage = "";
            DateTime date = DateTime.Now;
            int hour = date.Hour;
            if (hour < 12)
                welcomeMessage = "Good Morning!";
            else if (hour < 18)
                welcomeMessage = "Good afternoon!";
            else
                welcomeMessage = "Good evening!";
            welcomeMessage += Environment.NewLine;
            welcomeMessage += "The time is " + String.Format("{0:t}", date) + " on " +
                String.Format("{0:dddd, MMMM d, yyyy}", date);
            DateTime nextYear = new DateTime(date.Year + 1, 1, 1);
            int DaysLeft = nextYear.Subtract(date).Days;
            welcomeMessage += Environment.NewLine;
            welcomeMessage += DaysLeft + " more days until " + date.Year + 1;
            ViewData["WelcomeMessage"] = welcomeMessage;
            return View();
        }
        public IActionResult ShowPerson()
        {
            Person person = new Person()
            {
                FirstName = "Steve",
                LastName = "Effron",
                DateOfBirth = new DateTime(2000, 10, 5)
            };
            ViewData["Person"] = person;
            return View(person);
        }
        public IActionResult AddPerson()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                _context.SaveChanges();
                return RedirectToAction("List");
            }
            else
                return View();
        }
        public IActionResult List()
        {
            return View(_context.Persons.ToList());
        }
        public IActionResult Edit(int id)
        {
            var person = _context.Persons.SingleOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }
        [HttpPost]
        public IActionResult Edit(int id, Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }
            _context.Persons.Update(person);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int id ,Person person)
        {
            if(id!=person.Id)
            {
                return NotFound();
            }
            _context.Persons.Remove(person);
            _context.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = _context.Persons
                    .SingleOrDefault(p => p.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View("Details", person);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}
