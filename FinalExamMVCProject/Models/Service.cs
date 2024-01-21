using FinalExamMVCProject.Models.BaseModel;

namespace FinalExamMVCProject.Models
{
    public class Service:BaseEntity
    {
        public string Title { get; set; }
        public string?  ImgUrl{ get; set; }
    }
}
