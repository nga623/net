using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BaseStudy.Pages
{
    
    public class DisposeServiceModel : PageModel
    {
        private readonly Service1 _service1;
        private readonly Service2 _service2;
        private readonly IService3 _service3;

        public DisposeServiceModel(Service1 service1, Service2 service2, IService3 service3)
        {
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
        }

        public void OnGet()
        {
            _service1.Write("IndexModel.OnGet");
            _service2.Write("IndexModel.OnGet");
            _service3.Write("IndexModel.OnGet");
        }
    }
}
