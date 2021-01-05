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
    public class DepartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Department.Include(d => d.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Departments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.DeptNo == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID");
            return View();
        }

        public JsonResult GetResult(string searchBy, string searchValue)
        {
            List<Department> DepDetails = new List<Department>();
            if (searchBy == "DeptNo")
            {
                int no = Convert.ToInt32(searchValue);
                DepDetails = _context.Department.Where(e => e.DeptNo == no || searchValue == null).ToList();
                return Json(DepDetails);

            }
            else if (searchBy == "DeptName")
            {
                DepDetails = _context.Department.Where(e => e.DeptName.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(DepDetails);
            }
            else if (searchBy == "DeptLocation")
            {
                DepDetails = _context.Department.Where(e => e.DeptLocation.StartsWith(searchValue) || searchValue == null).ToList();
                return Json(DepDetails);
            }
            else if (searchBy == "EmployeeID")
            {
                int id = Convert.ToInt32(searchValue);
                DepDetails = _context.Department.Where(e => e.EmployeeID == id || searchValue == null).ToList();
                return Json(DepDetails);
            }
            else
            {
                var Dep = _context.Department;
                return Json(Dep);
            }
        }


        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeptNo,DeptName,DeptLocation,EmployeeID")] Department department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", department.EmployeeID);
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", department.EmployeeID);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("DeptNo,DeptName,DeptLocation,EmployeeID")] Department department)
        {
            if (id != department.DeptNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DeptNo))
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
            ViewData["EmployeeID"] = new SelectList(_context.Employee, "EmployeeID", "EmployeeID", department.EmployeeID);
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Department
                .Include(d => d.Employee)
                .FirstOrDefaultAsync(m => m.DeptNo == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int? id)
        {
            return _context.Department.Any(e => e.DeptNo == id);
        }
    }
}
