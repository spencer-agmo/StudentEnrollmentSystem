namespace StudentEnrollmentSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName{ get; set; }

        public string Room { get; set; }

        public int AvailableSlot { get; set; }  

    }
}
