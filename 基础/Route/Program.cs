using Route;
using Route.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddRouting(options =>
//    options.ConstraintMap.Add("noZeroes", typeof(NoZeroesRouteConstraint)));





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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller:slugify=Home}/{action:slugify=Index}/{id?}");

//var logger = app.Services.GetRequiredService<ILogger<Program>>();
//var timerCount = 0;

//app.Use(async (context, next) =>
//{
//    using (new AutoStopwatch(logger, $"Time {++timerCount}"))
//    {
//        await next(context);
//    }
//});

//// app.UseRouting();

//app.Use(async (context, next) =>
//{
//    using (new AutoStopwatch(logger, $"Time {++timerCount}"))
//    {
//        await next(context);
//    }
//});

//// app.UseAuthorization();

//app.Use(async (context, next) =>
//{
//    using (new AutoStopwatch(logger, $"Time {++timerCount}"))
//    {
//        await next(context);
//    }
//});

//app.MapGet("/",
//    () => "Timing Test."
//    );

 
app.Run();
