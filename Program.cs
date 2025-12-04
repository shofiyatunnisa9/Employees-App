using Microsoft.EntityFrameworkCore;
using EmployeesApp.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("DataKaryawan"));

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Karyawans.AddRange(
        new EmployeesApp.Models.Karyawan
        {
            NIK = 1,
            NamaLengkap = "Lionel Messi",
            JenisKelamin = "Laki-laki",
            TanggalLahir = new DateTime(1987, 6, 24),
            Alamat = "Jl. Achmad Yani No 89 Jakarta Pusat",
            Negara = "Argentina"
        },
        new EmployeesApp.Models.Karyawan
        {
            NIK = 2,
            NamaLengkap = "Cristiano Ronaldo",
            JenisKelamin = "Laki-laki",
            TanggalLahir = new DateTime(1985, 2, 5),
            Alamat = "Jl. Achmad Yani No 78 Jakarta Pusat",
            Negara = "Portugal"
        }
    );
    db.SaveChanges();
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Karyawan}/{action=Index}/{id?}");

app.Run();
