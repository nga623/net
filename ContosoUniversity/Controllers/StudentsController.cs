using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using System.Threading;

namespace ContosoUniversity.Controllers
{
    public class C
    {
        public async Task M()
        {
            var a = 1;
            await Task.Delay(2000);
            Thread.Sleep(3000);
            Console.WriteLine(a);
        }
    }
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;


        public async static Task TestConfigureAwait()
        {
            Console.WriteLine("TestConfigureAwait1 thread is " +
                              Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() =>
            {
                Thread.Sleep(3000);
                Console.WriteLine("task thread is " +
                                   Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine("TestConfigureAwait2 thread is " +
                               Thread.CurrentThread.ManagedThreadId);
        }
        public StudentsController(SchoolContext context)
        {
            //C c = new C();
            //var s = c.M();
            //s.ConfigureAwait(false);

            //Console.WriteLine("Main1:" + Thread.CurrentThread.ManagedThreadId);
            //Task t = TestConfigureAwait();
            //Console.WriteLine("Main2:" + Thread.CurrentThread.ManagedThreadId);
            //Console.ReadLine();

            _context = context;
        }

        // GET: Students
        //    public async Task<IActionResult> Index(
        //string sortOrder,
        //string currentFilter,
        //string searchString,
        //int? pageNumber)
        //    {
        //        ViewData["CurrentSort"] = sortOrder;
        //        ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //        ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

        //        if (searchString != null)
        //        {
        //            pageNumber = 1;
        //        }
        //        else
        //        {
        //            searchString = currentFilter;
        //        }

        //        ViewData["CurrentFilter"] = searchString;

        //        var students = from s in _context.Students
        //                       select s;
        //        if (!String.IsNullOrEmpty(searchString))
        //        {
        //            students = students.Where(s => s.LastName.Contains(searchString)
        //                                   || s.FirstMidName.Contains(searchString));
        //        }
        //        switch (sortOrder)
        //        {
        //            case "name_desc":
        //                students = students.OrderByDescending(s => s.LastName);
        //                break;
        //            case "Date":
        //                students = students.OrderBy(s => s.EnrollmentDate);
        //                break;
        //            case "date_desc":
        //                students = students.OrderByDescending(s => s.EnrollmentDate);
        //                break;
        //            default:
        //                students = students.OrderBy(s => s.LastName);
        //                break;
        //        }

        //        int pageSize = 3;
        //        return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(), pageNumber ?? 1, pageSize));
        //    }
        public async void M()
        {
            await Task.Delay(3000);
            Console.WriteLine(123);
        }
        public async Task M1()
        {

            await Task.Delay(3000);
            Console.WriteLine(123);
        }
        public async Task<int> M2()
        {

            await Task.Delay(3000);
            return 1;
        }
        public Thread T()
        {
            return new Thread(T2);
        }
        public Task T1()
        {
            return new Task(T2);
        }

        public void T2()
        {
            Console.WriteLine(Thread.GetCurrentProcessorId());
        }
        public async Task<IActionResult> Index(
     string sortOrder,
     string currentFilter,
     string searchString,
     int? pageNumber)
        {


            // M();
            //  var ss = M1();
            //  M2().Wait();
             M();
           // M1();
            var s2 =    M1();
           
            //Console.WriteLine(Thread.GetCurrentProcessorId());
            //T2();
            //T().Start();
            //T1().Start();



          //  Console.WriteLine(s2);

            var s23 = await _context.Students.ToListAsync();

            //显示加载
            //var studentss = _context.Students;
            //var s1 = studentss;
            //foreach (var s in studentss)
            //{
            //    _context.Entry(s).Collection(p => p.Enrollments).Load();
            //    //_context.Entry(s).Reference(p => p.Enrollments).Load();
            //    foreach (var item in s.Enrollments)
            //    {
            //        Console.WriteLine(item.StudentID);
            //    }
            //}

            //立即加载
            //var studentss = _context.Students.Include(p => p.Enrollments);
            //foreach (var item in studentss)
            //{
            //    Console.WriteLine(item.FirstMidName);
            //}
            //var studentss1 = _context.Students.Include(p => p.Enrollments);
            //foreach (var item in studentss1)
            //{
            //    Console.WriteLine(item.FirstMidName);
            //}
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] =
                String.IsNullOrEmpty(sortOrder) ? "LastName_desc" : "";
            ViewData["DateSortParm"] =
                sortOrder == "EnrollmentDate" ? "EnrollmentDate_desc" : "EnrollmentDate";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var students = from s in _context.Students
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            if (string.IsNullOrEmpty(sortOrder))
            {
                sortOrder = "LastName";
            }

            bool descending = false;
            if (sortOrder.EndsWith("_desc"))
            {
                sortOrder = sortOrder.Substring(0, sortOrder.Length - 5);
                descending = true;
            }

            if (descending)
            {
                students = students.OrderByDescending(e => EF.Property<object>(e, sortOrder));
            }
            else
            {
                students = students.OrderBy(e => EF.Property<object>(e, sortOrder));
            }

            int pageSize = 3;

            return View(await PaginatedList<Student>.CreateAsync(students.AsNoTracking(),
                pageNumber ?? 1, pageSize));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Course)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
    [Bind("EnrollmentDate,FirstMidName,LastName")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(student);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.DetectChanges();
                    Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {

            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
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
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }
    }
}
