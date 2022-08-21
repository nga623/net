using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BaseStudy;
using System.Diagnostics;

namespace BaseStudy.Pages
{
    
    public class IndexModel : PageModel
    {
        private readonly IMyDependency _myDependency;
        private readonly IEnumerable<IMyDependency> _myDependencys;
        private readonly ILogger<IndexModel> _logger;
       
        public void TestTask(Action<Task> t)
        {
            Task t1 = new Task(() =>
            {
                Console.WriteLine("1");
            });
            t.Invoke(t1);
        }
        public void TestTaskByIntArgument(Action<Task<int>> t)
        {
            Task<int> t1 = new Task<int>(TFunc);
            t.Invoke(t1);
            var ss = t1.Result;
        }
        public int TFunc()
        {
            return 1;
        }
        public void TestInt(Action<int> t)
        {
            t.Invoke(1);
            t(1);
        }


         
        public IndexModel
            (
             ILogger<IndexModel> logger
           , IMyDependency myDependency
           , IEnumerable<IMyDependency> myDependencys
            
            )
        {
            _myDependency = myDependency;
            _myDependencys = myDependencys;
            _logger = logger;
            

            // TestTaskByIntArgument((t2) => { t2.Start(); });
            TestTask((t2) => { t2.Start(); });
            //  TestInt((i) => Console.WriteLine(i));
            Trace.Assert(myDependency is DifferentDependency);
            Trace.Assert(myDependencys.ToArray()[0] is BaseStudy.MyDependency);
            Trace.Assert(myDependencys.ToArray()[1] is DifferentDependency);
        }

        public void OnGet()
        {
           
        }
    }
}