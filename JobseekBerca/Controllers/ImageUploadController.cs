using JobseekBerca.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace JobseekBerca.Controllers
{

    [EnableCors("AllowSpecificOrigin")]
    [ApiController]
    [Route("api/[controller]")]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly MyContext _myContext;

        public ImageUploadController(IWebHostEnvironment env, MyContext myContext)
        {
            _env = env;
            _myContext = myContext;
        }

        // Endpoint untuk upload gambar profil
        [HttpPost]
        public async Task<IActionResult> UploadProfileImage([FromForm] ProfileImageUploadRequest request)
        {
            // Cek apakah file ada di dalam request
            if (request.ProfileImage == null || request.ProfileImage.Length == 0)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Tidak ada file yang diupload",
                });
            }

            // Membuat nama file unik dengan UUID
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ProfileImage.FileName)}";
            var folderPath = Path.Combine("wwwroot/uploads/userProfiles");

            // Membuat folder jika belum ada
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Path file akhir
            var fullPath = Path.Combine(folderPath, uniqueFileName);

            // Menyimpan file ke disk
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await request.ProfileImage.CopyToAsync(stream);
            }

            // Membuat path relatif yang akan disimpan di database
            var imagePath = Path.Combine("uploads", "userProfiles", uniqueFileName).Replace("\\", "/");

            // Mencari profil yang sesuai dengan UserId
            var profile = await _myContext.Profiles.FirstOrDefaultAsync(p => p.userId == request.UserId);

            if (profile == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Profil tidak ditemukan",
                });
            }

            // Menghapus file lama jika ada
            if (!string.IsNullOrEmpty(profile.profileImage))
            {
                var oldImagePath = Path.Combine("wwwroot", profile.profileImage.Replace("/", "\\"));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            // Memperbarui path gambar di profil
            profile.profileImage = imagePath;

            // Menyimpan perubahan di database
            _myContext.Profiles.Update(profile);
            await _myContext.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "Gambar profil berhasil diupload",
                Data = profile
            });
        }


        public class ProfileImageUploadRequest
        {
            public string UserId { get; set; } // Id User yang terkait
            public IFormFile? ProfileImage { get; set; } // File gambar profil
        }

    }
}
