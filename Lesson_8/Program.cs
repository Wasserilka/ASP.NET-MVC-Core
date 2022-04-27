using Lesson_8;
using Lesson_8.Repositories;
using AutoMapper;
using FluentMigrator.Runner;
using Lesson_8.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
    .AddSQLite()
    .WithGlobalConnectionString(new ConnectionManager().ConnectionString)
    .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb
    .AddFluentMigratorConsole());

var mapperConfiguration = new MapperConfiguration(mp => mp.AddProfile(new MapperProfile()));
var mapper = mapperConfiguration.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

var scope = app.Services.CreateScope();
var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
runner.MigrateUp();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
