namespace FinalExamMVCProject.Areas.Manage.ViewModels.ServicesVMs
{
    public class ServiceUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImgUrl { get; set; }
    }
}
