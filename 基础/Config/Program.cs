using Config.Controllers;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.Configure<PositionOptions>(
    builder.Configuration.GetSection(PositionOptions.Position));

//合并服务集合
builder.Services
    .AddConfig(builder.Configuration)
    //.AddMyDependencyGroup()
    ;
builder.Services.AddControllers()
  .ConfigureApiBehaviorOptions(options =>
  {
      // To preserve the default behavior, capture the original delegate to call later.
      var builtInFactory = options.InvalidModelStateResponseFactory;

      options.InvalidModelStateResponseFactory = context =>
      {
          var logger = context.HttpContext.RequestServices
                              .GetRequiredService<ILogger<Program>>();

          // Perform logging here.
          // ...

          // Invoke the default behavior, which produces a ValidationProblemDetails
          // response.
          // To produce a custom response, return a different implementation of 
          // IActionResult instead.
          return builtInFactory(context);
      };
  });

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("MySubsection.json",
                       optional: true,
                       reloadOnChange: true);
});

builder.Services.AddW3CLogging(logging =>
{
    // Log all W3C fields
    logging.LoggingFields = W3CLoggingFields.All;

    logging.FileSizeLimit = 5 * 1024 * 1024;
    logging.RetainedFileCountLimit = 2;
    logging.FileName = "MyLogFile";
    logging.LogDirectory = @"C:\logs";
    logging.FlushInterval = TimeSpan.FromSeconds(2);
});

builder.Services.Configure<TopItemSettings>(TopItemSettings.Month,
    builder.Configuration.GetSection("TopItem:Month"));
builder.Services.Configure<TopItemSettings>(TopItemSettings.Year,
    builder.Configuration.GetSection("TopItem:Year"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//开启http日志记录器
app.UseHttpLogging();
app.UseW3CLogging();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
