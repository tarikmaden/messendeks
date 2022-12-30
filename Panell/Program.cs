using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Panell.Models;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(500);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<SayfaContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "NetCoreMvc.Auth";
    options.LoginPath = "/panel/login";
    options.AccessDeniedPath = "/panel/login";
});

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(1000);
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/Error1","?code={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//route ayari

app.MapControllerRoute(
    name: "Index",
    pattern: "ana-sayfa",
    defaults: new { controller = "Home", action = "Index" });

app.MapControllerRoute(
    name: "Kurumsal",
    pattern: "kurumsal",
    defaults: new { controller = "Home", action = "Kurumsal" });

app.MapControllerRoute(
    name: "Hakkimizda",
    pattern: "hakkimizda/{id}",
    defaults: new { controller = "Home", action = "Hakkimizda" });

app.MapControllerRoute(
    name: "Hizmetlerimiz",
    pattern: "hizmetlerimiz",
    defaults: new { controller = "Home", action = "Hizmetlerimiz" });

app.MapControllerRoute(
    name: "Haberler",
    pattern: "Haberler",
    defaults: new { controller = "Home", action = "Haberler" });

app.MapControllerRoute(
    name: "Iletisim",
    pattern: "iletisim",
    defaults: new { controller = "Home", action = "Iletisim" });

app.MapControllerRoute(
    name: "Blog",
    pattern: "blog",
    defaults: new { controller = "Home", action = "Blog" });

app.MapControllerRoute(
    name: "Haber",
    pattern: "haber/{id}",
    defaults: new { controller = "Home", action = "Haber" });

app.MapControllerRoute(
    name: "Arama_sonuclari",
    pattern: "arama-sonuclari",
    defaults: new { controller = "Home", action = "Arama_sonuclari"});

app.MapControllerRoute(
    name: "Yatirimci_endeksi_gorusu",
    pattern: "yatirimci-gorusu-endeksi",
    defaults: new { controller = "Home", action = "Yatirimci_endeksi_gorusu"});

app.MapControllerRoute(
    name: "Yapim_asamasinda",
    pattern: "yapim-asamasinda",
    defaults: new { controller = "Home", action = "Yapim_asamasinda"});

app.MapControllerRoute(
    name: "Dil",
    pattern: "dil",
    defaults: new { controller = "Home", action = "Dil" });

app.MapControllerRoute(
    name: "Iletisim_send",
    pattern: "iletisim-send",
    defaults: new { controller = "Home", action = "Iletisim_send" });



app.Run();
