using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSM.Data;
using SMSM.Models;

namespace SMSM.Controllers
{
    public class StudentInfoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentInfoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StudentInfoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.StudentInfo.ToListAsync());
        }

		// GET: StudentInfoes/ShowSearchForm
		public async Task<IActionResult> ShowSearchForm()
		{
			return View();
		}

        // GET: StudentInfoes/ShowSearchResults
		public async Task<IActionResult> ShowSearchResults(String SearchDetails)

        {
            return View("Index", await _context.StudentInfo.Where(j => j.Name.Contains(SearchDetails)).ToListAsync());

        }

		// GET: StudentInfoes/Details/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StudentInfo == null)
            {
                return NotFound();
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentInfo == null)
            {
                return NotFound();
            }

            return View(studentInfo);
        }

        // GET: StudentInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SubjectName,SubjectCode,Course,DOB")] StudentInfo studentInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentInfo);
        }

        // GET: StudentInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StudentInfo == null)
            {
                return NotFound();
            }

            var studentInfo = await _context.StudentInfo.FindAsync(id);
            if (studentInfo == null)
            {
                return NotFound();
            }
            return View(studentInfo);
        }

        // POST: StudentInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SubjectName,SubjectCode,Course,DOB")] StudentInfo studentInfo)
        {
            if (id != studentInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentInfoExists(studentInfo.Id))
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
            return View(studentInfo);
        }

        // GET: StudentInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StudentInfo == null)
            {
                return NotFound();
            }

            var studentInfo = await _context.StudentInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentInfo == null)
            {
                return NotFound();
            }

            return View(studentInfo);
        }

        // POST: StudentInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StudentInfo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StudentInfo'  is null.");
            }
            var studentInfo = await _context.StudentInfo.FindAsync(id);
            if (studentInfo != null)
            {
                _context.StudentInfo.Remove(studentInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentInfoExists(int id)
        {
          return _context.StudentInfo.Any(e => e.Id == id);
        }
    }
}
