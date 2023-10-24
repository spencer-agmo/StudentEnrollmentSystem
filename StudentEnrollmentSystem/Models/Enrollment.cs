using StudentEnrollmentSystem.Models.Auth;

namespace StudentEnrollmentSystem.Models
{
    public class Enrollment
    {
       
        public int EnrollmentId { get; set; }
        public int Status { get; set; }

        public int CourseId { get; set; }
        public string  UserId { get; set; }    
    }
}
