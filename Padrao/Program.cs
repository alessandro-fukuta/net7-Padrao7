using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Oficina7.Data;
using Oficina7.Models;

using Coravel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Coravel.Mailer.Mail.Helpers;
using Oficina7;

Publica.Sistema_Nome = "OFICINA 7"; 
Publica.Sistema_Versao = "Plus";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IEmailSender, EmailSender>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// PARA MYSQL 8

/*
builder.Services.AddEntityFrameworkMySQL().AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
});
*/

// PARA SQL SERVER
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});




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

app.Run();
