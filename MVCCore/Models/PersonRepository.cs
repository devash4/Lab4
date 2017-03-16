using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCCore.Models
{
    public class PersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public PersonRepository()
        {

        }
        public List<Person> PersonList { get; set; }

        public void Add(Person person)
        {
            // ProductList.Add(product);
            _context.Add(person);
            _context.SaveChanges();
        }
    }
}
