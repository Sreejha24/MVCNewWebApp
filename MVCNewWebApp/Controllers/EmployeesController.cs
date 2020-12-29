using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCNewWebApp.Data;
using MVCNewWebApp.Models;

namespace MVCNewWebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        public JsonResult GetResult(string searchBy, string searchValue)
        {
            List<Employee> empDetails = new List<Employee>();
            if (searchBy == "EmployeeID")
            {
                int id = Convert.ToInt32(searchValue);
                empDetails = _context.Employee.Where(e => e.EmployeeID == id || searchValue == null).ToList();
                return Json(empDetails);

            }
            else if (searchBy == "FirstName")
            {
                empDetails = _context.Employee.Where(e => e.FirstName.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(empDetails);
            }
            else if (searchBy == "LastName")
            {
                empDetails = _context.Employee.Where(e => e.LastName.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(empDetails);
            }
            else if (searchBy == "Email")
            {
                empDetails = _context.Employee.Where(e => e.Email.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(empDetails);
            }
            else if (searchBy == "City")
            {
                empDetails = _context.Employee.Where(e => e.City.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(empDetails);
            }
            else if (searchBy == "Mobile")
            {
                long mobile = long.Parse(searchValue);
                empDetails = _context.Employee.Where(e => e.Mobile == mobile || searchValue == null).ToList();
                return Json(empDetails);
            }
            else
            {
                var emp = _context.Employee;
                return Json(emp);
            }
        }


        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,FirstName,LastName,Email,City,Mobile")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("EmployeeID,FirstName,LastName,Email,City,Mobile")] Employee employee)
        {
            if (id != employee.EmployeeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeID))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int? id)
        {
            return _context.Employee.Any(e => e.EmployeeID == id);
        }
    }
}
