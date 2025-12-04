using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeesApp.Models
{
    public class Karyawan
    {
        [Key]
        [Required]
        public int NIK { get; set; }

        [Required]
        [Display(Name = "Nama Lengkap")]
        public string NamaLengkap { get; set; }

        [Display(Name = "Jenis Kelamin")]
        public string JenisKelamin { get; set; }

        [Display(Name = "Tanggal Lahir")]
        public DateTime? TanggalLahir { get; set; }

        public string Alamat { get; set; }

        public string Negara { get; set; }

        // Umur dihitung otomatis dari TanggalLahir
        public int? Umur
        {
            get
            {
                if (!TanggalLahir.HasValue) return null;

                var today = DateTime.Today;
                var age = today.Year - TanggalLahir.Value.Year;
                if (TanggalLahir.Value.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }
    }
}
