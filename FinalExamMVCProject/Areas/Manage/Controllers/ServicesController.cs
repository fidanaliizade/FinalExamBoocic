using FinalExamMVCProject.Areas.Manage.ViewModels.ServicesVMs;
using FinalExamMVCProject.DAL;
using FinalExamMVCProject.Helpers;
using FinalExamMVCProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinalExamMVCProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ServicesController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _db.Services.ToListAsync();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Service service = new Service()
            {
                Title = vm.Title,
                ImgUrl = vm.Image.Upload(_env.WebRootPath, @"\Upload\Service\")
            };
            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            Service service = await _db.Services.FindAsync(id);
            ServiceUpdateVM updated = new ServiceUpdateVM()
            {
                Title = service.Title,
                ImgUrl = service.ImgUrl
            };
            return View(updated);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ServiceUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Service service = await _db.Services.FindAsync(vm.Id);
            if (service == null)
            {
                throw new Exception("Service is null;");
            }
            if (service.ImgUrl != null)
            {
                service.ImgUrl = vm.Image.Upload(_env.WebRootPath, @"\Upload\Service\");
            }
            if (service.ImgUrl != null)
            {
                if (!vm.Image.CheckType("image/"))
                {
                    ModelState.AddModelError("Image", "Enter rigth image format.");
                }
                if (!vm.Image.CheckLength(3000))
                {
                    ModelState.AddModelError("Image", "Enter max 3mb image .");
                }
            }
            if (vm.Id <= 0)
            {
                throw new Exception("Id is can not zero or negative.");
            }
            service.Title = vm.Title;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public  IActionResult Delete(int id)
        {
            var service =  _db.Services.FirstOrDefault(x=>x.Id==id);
            if(service == null)
            {
                throw new Exception("service is null.");
            }
            _db.Services.Remove(service);
             _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
