//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.

//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

using Microsoft.AspNetCore.Rewrite;
using WebApi;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.UseStaticFiles();
//app.Run(context => context.Response.WriteAsync(
//    $"Rewrittefdddff   or Redirected Url: " +
//    $"{context.Request.Path + context.Request.QueryString}"));
app.Run();
