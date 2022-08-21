using BaseStudy;
using BaseStudy.Pages;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
 
// Add services to the container.
#region MyDependency
#region IMyDependency
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IMyDependency, MyDependency>();
builder.Services.AddSingleton<IMyDependency, DifferentDependency>();
#endregion 

#region DisposeService

builder.Services.AddScoped<Service1>();
builder.Services.AddTransient<Service2>(); 

var myKey = builder.Configuration["MyKey"];
builder.Services.AddSingleton<IService3>(sp => new Service3(myKey));
#endregion 
#endregion



var app = builder.Build();
//app.Use(async (context, next) =>
//{
//    // Do work that doesn't write to the Response.
//    await next.Invoke();
//    // Do logging or other work that doesn't write to the Response.
//});

//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Hello from 2nd delegate.");
//});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
 
app.UseAuthorization();

app.MapRazorPages();
 

 
app.Run();


