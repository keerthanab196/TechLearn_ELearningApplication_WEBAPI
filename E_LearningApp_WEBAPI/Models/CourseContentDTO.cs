namespace E_LearningApp_WEBAPI.Models
{
    public class CourseContentDTO
    {
        public int ContentId { get; set; }

        public int CourseId { get; set; }

        public string Title { get; set; } = null!;

        public string ContentType { get; set; } = null!;

        public string Filepath { get; set; } = null!;


    }
}
