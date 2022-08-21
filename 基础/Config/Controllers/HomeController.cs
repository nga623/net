using Config.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Config.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration Configuration;
        private readonly PositionOptions _options;
        private readonly IConfiguration Config;
        private readonly IConfiguration Config1;
        private readonly PositionOptions _snapshotOptions;
        private readonly IOptionsMonitor<PositionOptions> _optionsDelegate;
        private readonly TopItemSettings _monthTopItem;
        private readonly TopItemSettings _yearTopItem;
        
        public PositionOptions? positionOptions { get; private set; }
        public HomeController
            (
            ILogger<HomeController> logger
          , IConfiguration configuration
          , IOptions<PositionOptions> options
         , IOptionsSnapshot<PositionOptions> snapshotOptionsAccessor
            , IOptionsMonitor<PositionOptions> optionsDelegate
            , IOptionsSnapshot<TopItemSettings> namedOptionsAccessor

            )
        {
            _logger = logger;
            Configuration = configuration;
            _options = options.Value;
            Config = configuration.GetSection("section1");
            Config1 = configuration.GetSection("section2:subsection0");
            _snapshotOptions = snapshotOptionsAccessor.Value;
            _optionsDelegate = optionsDelegate;
            _monthTopItem = namedOptionsAccessor.Get(TopItemSettings.Month);
            _yearTopItem = namedOptionsAccessor.Get(TopItemSettings.Year);
        }
       // [Route("home/{id:int:min(1)}")]
        public IActionResult Index()
        {
              return NoContent();
            //_logger.LogInformation("About page visited at {DT}",
            //DateTime.UtcNow.ToLongTimeString());
            //var numberKey = Configuration.GetValue<int>("NumberKey", 99);
            //var myKeyValue = Configuration["MyKey"];
            //var title = Configuration["Position:Title"];
            //var name = Configuration["Position:Name"];
            //var defaultLogLevel = Configuration["Logging:LogLevel:Default"];


            //return Content($"MyKey value: {myKeyValue} \n" +
            //               $"Title: {title} \n" +
            //               $"Name: {name} \n" +
            //               $"Default Log Level: {defaultLogLevel}");

        }

        public ContentResult Index1()
        {
            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

            return Content($"Title: {positionOptions.Title} \n" +
                           $"Name: {positionOptions.Name}");
        }
        public ContentResult Index2()
        {
            positionOptions = Configuration.GetSection(PositionOptions.Position)
                                                         .Get<PositionOptions>();

            return Content($"Title: {positionOptions.Title} \n" +
                           $"Name: {positionOptions.Name}");
        }

        public ContentResult Index3()
        {

            return Content($"Title: {_options.Title} \n" +
                           $"Name: {_options.Name}");
        }
        public ContentResult Index4()
        {
            return Content(
                    $"section1:key0: '{Config["key0"]}'\n" +
                    $"section1:key1: '{Config["key1"]}'");
        }
        public ContentResult Index5()
        {
            return Content(
                    $"section2:subsection0:key0 '{Config1["key0"]}'\n" +
                    $"section2:subsection0:key1:'{Config1["key1"]}'");
        }

        public ContentResult Index6()
        {
            string s = "";
            var selection = Configuration.GetSection("section2");
            if (!selection.Exists())
            {
                throw new Exception("section2 does not exist.");
            }
            var children = selection.GetChildren();

            foreach (var subSection in children)
            {
                int i = 0;
                var key1 = subSection.Key + ":key" + i++.ToString();
                var key2 = subSection.Key + ":key" + i.ToString();
                s += key1 + " value: " + selection[key1] + "\n";
                s += key2 + " value: " + selection[key2] + "\n";
            }
            return Content(s);
        }
        public ContentResult Index7()
        {
            return Content($"Option1: {_snapshotOptions.Name} \n" +
                           $"Option2: {_snapshotOptions.Title}");
        }
        public ContentResult Index8()
        {
            return Content($"Option1: {_optionsDelegate.CurrentValue.Name} \n" +
                           $"Option2: {_optionsDelegate.CurrentValue.Title}");
        }
        public ContentResult Index9()
        {
            return Content($"Month:Name {_monthTopItem.Name} \n" +
                           $"Month:Model {_monthTopItem.Model} \n\n" +
                           $"Year:Name {_yearTopItem.Name} \n" +
                           $"Year:Model {_yearTopItem.Model} \n");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class PositionOptions
    {
        public const string Position = "Position";

        public string Title { get; set; } = String.Empty;
        public string Name { get; set; } = String.Empty;
    }
    public class TopItemSettings
    {
        public const string Month = "Month";
        public const string Year = "Year";

        public string Name { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
    }
}