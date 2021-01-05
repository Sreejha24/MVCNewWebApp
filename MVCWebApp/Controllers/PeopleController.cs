using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCWebApp.Data;
using MVCWebApp.Models.Views;

namespace MVCWebApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            return View(await _context.Person.ToListAsync());
        }
        public async Task<IActionResult> RawQuery(int id)
        {
            var sql = "SELECT * FROM People Where PersonID={0}";
            var data = _context.Person.FromSqlRaw(sql, id).FirstOrDefault();
            return View(data);
        }

        public IActionResult RawQueryComplex(int id)
        {
            IList<PersonAddressView> list = new List<PersonAddressView>();
            using(var conn =_context.Database.GetDbConnection())
            {
                conn.Open();
                using(var command = conn.CreateCommand())
                {
                    var sql = "SELECT P.PersonID,P.FirstName,P.LastName,P.Email,P.Mobile,P.AddressID,A.AddressLine,A.City,A.Pin FROM People P,PAddresss A" +
                        "WHERE P.AddressID = A.AddressID";
                    command.CommandText = sql; 
                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                var record = new PersonAddressView()
                                {
                                    PersonID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    LastName = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Mobile = reader.GetInt64(4),
                                    AddressId = reader.GetInt32(5),
                                    AddressLine = reader.GetString(6),
                                    City = reader.GetString(7),
                                    Pin = reader.GetInt32(8)
                                };
                                list.Add(record);
                            }
                        }
                    }
                }
                conn.Close();
            }
            return View(list);
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,Email,Gender,Mobile,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PersonID,FirstName,LastName,Email,Gender,Mobile,Address")] Person person)
        {
            if (id != person.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var person = await _context.Person.FindAsync(id);
            _context.Person.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int? id)
        {
            return _context.Person.Any(e => e.PersonID == id);
        }
    }
}
