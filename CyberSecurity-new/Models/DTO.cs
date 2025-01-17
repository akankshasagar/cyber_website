namespace CyberSecurity_new.Models
{
    public class AddCourseDto
    {
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ImagePath { get; set; }
        public List<AddModuleDto> Modules { get; set; }
    }

    public class AddModuleDto
    {
        public string Module_Name { get; set; }
        public List<AddTopicDto> Topics { get; set; }
    }

    public class AddTopicDto
    {
        public string Topic_Name { get; set; }
        public string Topic_Description { get; set; }
        public string T_ImagePath { get; set; } // Optional
    }

}
