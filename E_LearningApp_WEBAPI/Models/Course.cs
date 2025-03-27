using System;
using System.Collections.Generic;

namespace E_LearningApp_WEBAPI.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdated { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<CourseContent> CourseContents { get; set; } = new List<CourseContent>();

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
