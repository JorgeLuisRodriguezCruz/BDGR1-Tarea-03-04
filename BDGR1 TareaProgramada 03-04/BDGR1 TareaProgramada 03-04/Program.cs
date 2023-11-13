using BDGR1_TareaProgramada_03_04.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AppDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=CargaDatos}/{action=Cargar}/{id?}");

    //pattern: "{controller=Acceso}/{action=InicioSesion}/{id?}"); 
    
    //pattern: "{controller=MunuAdmin}/{action=Editar}/{id?}"); 
    //pattern: "{controller=MunuAdmin}/{action=ListaEmpleado}/{id?}");

    //pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
