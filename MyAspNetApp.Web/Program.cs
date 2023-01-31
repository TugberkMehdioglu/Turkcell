using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using MyAspNetApp.Web.Filters;
using MyAspNetApp.Web.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("MyConnectionWithSqlAuth"));
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Bu bir dosya yolu oldugundan tekrar tekrar nesne orneginin uretilmesine gerek yok bu yuzden Singleton yaptik
//parametre olarak: bu interface'e karsilik somut bir sinif vermem lazim, bunun icin PhysicalFileProvider kullandik
//PhysicalFileProvider'a parametre olarak: root vermemizi istiyor, ben sana dosyalari/klasorleri vericem ama ben neye gore aricam/hangi projeye gore aricam diyo
//bende ona: Benim projenin kok klasorune al dicem, yani bu projenin bulundugu klasor senin root yani ana klasorun olucak
//Directory.GetCurrentDirectory() sayesinde: Projemin çalışmış olduğu klasörü verir
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.AddScoped<NotFoundFilter>();

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


app.MapControllers(); //Controller'lara MAP'leme görevi görsün diye bunu çağırdık.

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
