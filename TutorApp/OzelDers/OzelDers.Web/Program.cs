using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OzelDers.Business.Abstract;
using OzelDers.Business.Concrete;
using OzelDers.Data.Abstract;
using OzelDers.Data.Concrete;
using OzelDers.Data.Concrete.EfCore.Context;
using OzelDers.Entity.Concrete.Identity;
using OzelDers.Web.EmailServices.Abstract;
using OzelDers.Web.EmailServices.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<OzelDersContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<OzelDersContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options => 
{
    #region PasswordSettings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;

    #endregion
    #region LoginSetting
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

    #endregion
    #region UserSettings
    options.User.RequireUniqueEmail = true;

    #endregion
    #region SignInSettings
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    #endregion
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/account/login";
    options.LogoutPath = "/account/logout";
    options.AccessDeniedPath = "/account/accesdenied";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.Cookie = new CookieBuilder 
    {
        HttpOnly = true,
        Name = ".OzelDers.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
});


builder.Services.AddScoped<IEmailSender, SmtpEmailSender>(x => new SmtpEmailSender(
    builder.Configuration["EmailSender:Host"],
    builder.Configuration.GetValue<int>("EmailSender:Port"),
    builder.Configuration.GetValue<bool>("EmailSender:EnableSSl"),
    builder.Configuration["EmailSender:UserName"],
    builder.Configuration["EmailSender:Password"]

    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IBranchService, BranchManager>();
builder.Services.AddScoped<ILessonService, LessonManager>();
builder.Services.AddScoped<ITeacherService, TeacherManager>();
builder.Services.AddScoped<IStudentService, StudentManager>();
builder.Services.AddScoped<ICommentService, CommentManager>();




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
  
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


//studetn areasındaki detay sayfasına gidicek
app.MapAreaControllerRoute(
    name: "teacherdetails",
    areaName: "Students",
    pattern: "ogretmenler/{teacherid}",
    defaults: new { controller = "Home", action = "TeacherDetails" }
    );

//anasayfadaki gelen derslerin url alıp öğretmene göre falan
app.MapControllerRoute(
    name: "teachers",
    pattern: "ders/{lessonurl?}",
    defaults: new { controller = "OzelDers", action = "TeacherList" }
    );



//teacher areasındaki detay için
app.MapAreaControllerRoute(
    name: "studentdetails",
    areaName: "Teachers",
    pattern: "ogrenciler/{studentid}",
    defaults: new { controller = "Home", action = "StudentDetails" }
    );
//-------------------
app.MapAreaControllerRoute(
    name: "students",
    areaName: "Teachers",
    pattern: "studentders/{studentlessonurl?}",
    defaults: new { controller = "Home", action = "StudentList" }
    );


//arealarımıza erişmek için

app.MapAreaControllerRoute(
    name: "Admin",
    areaName: "Admin",//Areasn i�indeki Admin klas��rnden bahsediyoruz
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Teachers",
    areaName: "Teachers",
    pattern: "Teachers/{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "Students",
    areaName: "Students",
    pattern: "Students/{controller=Home}/{action=Index}/{id?}");








app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
