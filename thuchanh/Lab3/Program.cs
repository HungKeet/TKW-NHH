using MyWebApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<StudentRepository>();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
    app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute("StudentList", "Admin/Student/List", new { controller = "Student", action = "Index" });
app.MapControllerRoute("StudentAdd", "Admin/Student/Add", new { controller = "Student", action = "Create" });
app.MapControllerRoute("Students", "Students/List", new { controller = "Student", action = "Index" });
app.MapControllerRoute("default", "{controller=Student}/{action=Index}/{id?}");
app.Run();
