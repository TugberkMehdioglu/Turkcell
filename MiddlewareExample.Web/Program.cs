using MiddlewareExample.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

#region Use ve Run kullanımı
////app.Use() method'uyla Middleware'lerimizi olusturucaz.
////Use dedikten sonra 2 tane onemli parametre alan method tanimliyorum
////next ile beraber Middleware'e gelen istegi degerlendiricez sonra next method'uyla beraber gelen istegi bir sonraki Middleware'e aktaricaz
//app.Use(async (context, next) =>
//{
//    //Direkt olarak Response'a bir yazı yazalım
//    //async olduğundan await kullanıyoruz
//    await context.Response.WriteAsync("Before 1. Middleware\n"); //\n ile bir aşa satıra geçer
//    await next();
//    await context.Response.WriteAsync("After 1. Middleware\n");

//    //Bu Middleware'e Request geldiğinde 26.satırdakini yazdırtıcak sonra next method'uyla beraber bir sonrakine aktarıcak, Response tarafında da 28.satırdakini yazdırtıcak
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Before 2. Middleware\n");
//    await next();
//    await context.Response.WriteAsync("After 2. Middleware\n");
//});

////Birde sonlandırıcı bir Middleware oluşturucam

////Run method'u sonlandırıcı bir Middleware'dir. Bu tek bir ifade alır oda context'dir.
////Neden next parametresi almaz? çünkü bu sonlandırıcı Middleware olduğundan gelen Request'i bir sonraki Middleware'e yollama derdi yok, burada Request biticek artık yani Run method'unun aşağısındaki Middleware'lere uğramicak Request (UseRouting - UseAuthorization Middleware'lerine uğramicak Request'imiz).
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("Terminal 3. Middleware\n");
//}); 
#endregion

//Use method'u her istek geldiğinde çalışır yani hangi URL'e istek yaptın önemli değil, nereye istek yaparsan yap hep Use çalışır ve hiçbir zaman Action method'ların çalışmaz çünkü Run ile sonlandırıcı Middleware'ini yaptın.
//Peki ben sadece belli path'ler yazdığımda URL'de, o zaman bu Middleware'im çalışsın dersen işte burda Map method'u devreye giriyor.
//baseURL/Ornek yazıldığında buradaki Middleware'im çalışır

#region Map Method Kullanımı
//app.Map("/Ornek", app =>
//{
//    //daha ileri gitmesin dedik ve sonklandırıcı Middleware yazdık
//    app.Run(async (context) =>
//    {
//        await context.Response.WriteAsync("Ornek url'i icin Middleware");
//    });
//    //Burası sadece /Ornek path'ine istek olduğunda çalışıcak olan sonlandırıcı Middleware'imi içerir.
//});
#endregion


#region MapWhen Method Kullanımı
////MapWhen method'u biraz daha detaylı bir şekilde bir Middleware yazmak istediğimizde kullanıyoruz, yani Middleware'i yakalamak istediğimizde
////Ör. Bir Request'in QueryString'inde bir değer geçtiğinde Middleware'im çalışsın gibi bir senaryo olabilir
//app.MapWhen(context => context.Request.Query.ContainsKey("name"), app =>
//{
//    //Request'in QueryString'lerinde name diye bir key içerirse, app ile beraber aşağıdaki Middleware'im çalışsın demiş olduk
//    app.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("Before 1. Middleware\n");
//        await next();
//        await context.Response.WriteAsync("After 1. Middleware\n");
//    });
//    app.Run(async (context) =>
//    {
//        await context.Response.WriteAsync("Terminal 3. Middlware\n");
//    });
//}); 
#endregion

//Bu sayede Middleware'imiz aktif hale gelmiş oldu
app.UseMiddleware<WhiteIPAddressControlMiddleware>();
//uygulamada hangi sayfaya gitmeye çalışırsan çalış, her seferinde çalışır bu Middleware

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
