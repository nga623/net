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
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BenchmarkDotNet.Attributes;

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
        [Benchmark]
        public async Task<IActionResult> Index(
     string sortOrder,
     string currentFilter,
     string searchString,
     int? pageNumber)
        {


            var ss = _context.Students.TagWith("Use hint: robust plan").ToList();



            //使用 ChangeTracker.Entries 访问所有跟踪的实体
            //1 如果不加 _context.Students.ToList();无法访问改变的跟踪实体
            //2 _context.ChangeTracker.Entries<Enrollment>()也可使用泛型
            //var s1 = _context.Students.ToList();
            //foreach (var entityEntry in _context.ChangeTracker.Entries())
            //{
            //    Console.WriteLine($"Found {entityEntry.Metadata.Name} entity with ID {entityEntry.Property("ID").CurrentValue}");
            //}

            //封装crud
            //_context.ChangeTracker.TrackGraph(
            //    student, node =>
            //    {
            //        var propertyEntry = node.Entry.Property("Id");
            //        var keyValue = (int)propertyEntry.CurrentValue;

            //        if (keyValue == 0)
            //        {
            //            node.Entry.State = EntityState.Added;
            //        }
            //        else if (keyValue < 0)
            //        {
            //            propertyEntry.CurrentValue = -keyValue;
            //            node.Entry.State = EntityState.Deleted;
            //        }
            //        else
            //        {
            //            node.Entry.State = EntityState.Modified;
            //        }

            //        Console.WriteLine($"Tracking {node.Entry.Metadata.DisplayName()} with key value {keyValue} as {node.Entry.State}");
            //    });

            //_context.SaveChanges();


            //显示跟踪
            // var attach = _context.Attach(new Student { LastName = "123", FirstMidName = "233" });
            //  _context.SaveChanges(); //实际是新增实体
            //  var update = _context.Update(new Student { LastName = "1", FirstMidName = "2" });
            // _context.SaveChanges(); //实际是新增实体

            //删除posts集合
            //var s23 = _context.Students
            //    .Where(p => p.LastName == "Justice")
            //    .Include(p => p.Enrollments)
            //    .First();
            //_context.ChangeTracker.DetectChanges();
            //Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
            //s23.Enrollments.Clear();
            //_context.SaveChanges();

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
                    InsertOrUpdate(student);
                    // _context.Add(student);
                    //  await _context.SaveChangesAsync();
                    // _context.ChangeTracker.DetectChanges();
                    //  Console.WriteLine(_context.ChangeTracker.DebugView.LongView);
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
        public void InsertOrUpdate(Student student)
        {

            var existingBlog = _context.Students.Find(student.ID);
            if (existingBlog == null)
            {
                _context.Add(student);
            }
            else
            {
                //  _context.Update(student);
                _context.Entry(existingBlog).CurrentValues.SetValues(student);
            }

            _context.SaveChanges();
        }
        public void InsertUpdateOrDeleteGraph(Student student)
        {
            var existingBlog = _context.Students
                .Include(b => b.Enrollments)
                .FirstOrDefault(b => b.ID == student.ID);

            if (existingBlog == null)
            {
                _context.Add(student);
            }
            else
            {
                _context.Entry(existingBlog).CurrentValues.SetValues(student);
                foreach (var post in student.Enrollments)
                {
                    var existingPost = existingBlog.Enrollments
                        .FirstOrDefault(p => p.EnrollmentID == post.EnrollmentID);

                    if (existingPost == null)
                    {
                        existingBlog.Enrollments.Add(post);
                    }
                    else
                    {
                        _context.Entry(existingPost).CurrentValues.SetValues(post);
                    }
                }

                foreach (var post in existingBlog.Enrollments)
                {
                    if (!student.Enrollments.Any(p => p.EnrollmentID == post.EnrollmentID))
                    {
                        _context.Remove(post);
                    }
                }
            }

            _context.SaveChanges();
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
                    InsertOrUpdate(student);
                    // _context.Update(student);
                    // await _context.SaveChangesAsync();
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
