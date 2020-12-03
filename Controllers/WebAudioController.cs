using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebAudioPlatform.Data;

namespace WebAudioPlatform.Controllers
{
    public class WebAudioController : Controller
    {
        private IWebHostEnvironment _env;
        private string _dir;

        public WebAudioController(IWebHostEnvironment env)
        {
            _env = env;
            _dir = _env.ContentRootPath;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult BufferedSingleFileUploadPhysical()
        {
            return View();
        }

        public IActionResult UploadSingleFile(IFormFile file)
        {
            
            using (var fileStream = new FileStream(Path.Combine(_dir, "file.mp3"), FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UploadMultipleFiles(IEnumerable<IFormFile> files)
        {
            int i = 0;
            foreach(var file in files)
            {
                using (var fileStream = new FileStream(Path.Combine(_dir, $"file{i++}.mp3"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
                
            }

            return RedirectToAction("Index");

        }

        public IActionResult FileInModel(UploadFileModel uploadfile)
        {
            using (var fileStream = new FileStream(
                Path.Combine(_dir, $"{uploadfile.name}.mp3"), 
                FileMode.Create, 
                FileAccess.Write))
            {
                uploadfile.File.CopyTo(fileStream);
            }
            return RedirectToAction("Index");
        }
    }
}
