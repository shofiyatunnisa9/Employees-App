using Microsoft.AspNetCore.Mvc;
using EmployeesApp.Data;
using EmployeesApp.Models;

namespace EmployeesApp.Controllers
{
    public class KaryawanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KaryawanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ============ MONITORING ============
        // Filter berdasarkan NIK dan Nama untuk tampilan monitoring
        public IActionResult Index(int? nik, string? nama)
        {
            var query = _context.Karyawans.AsQueryable();

            if (nik.HasValue)
            {
                query = query.Where(d => d.NIK == nik.Value);
            }

            if (!string.IsNullOrWhiteSpace(nama))
            {
                query = query.Where(d => d.NamaLengkap.Contains(nama, StringComparison.OrdinalIgnoreCase));
            }

            var data = query.ToList();
            return View(data);
        }

        // ============ DETAIL ============
        public IActionResult Details(int id)
        {
            var data = _context.Karyawans.Find(id);
            if (data == null) return NotFound();

            return View(data);
        }

        // ============ TAMBAH DATA ============
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Karyawan model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Karyawans.Add(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ============ EDIT DATA ============
        public IActionResult Edit(int id)
        {
            var data = _context.Karyawans.Find(id);
            if (data == null) return NotFound();

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(Karyawan model)
        {
            if (!ModelState.IsValid)
                return View(model);

            _context.Karyawans.Update(model);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // ============ DELETE ============
        public IActionResult Delete(int id)
        {
            var data = _context.Karyawans.Find(id);
            if (data == null) return NotFound();

            return View(data); // ke halaman konfirmasi
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var data = _context.Karyawans.Find(id);
            if (data == null) return NotFound();

            _context.Karyawans.Remove(data);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
