using FinalExamMVCProject.DAL;
using Microsoft.AspNetCore.Mvc;

namespace FinalExamMVCProject.Areas.Manage.Controllers
{
    public class ServicesController : Controller
    {
        private readonly AppDbContext _db;

        public ServicesController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
