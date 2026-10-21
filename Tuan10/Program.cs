var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "admin_student_list",
    pattern: "Admin/Student/List",
    defaults: new { controller = "Student", action = "Index" });

app.MapControllerRoute(
    name: "admin_student_add",
    pattern: "Admin/Student/Add",
    defaults: new { controller = "Student", action = "Create" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();
