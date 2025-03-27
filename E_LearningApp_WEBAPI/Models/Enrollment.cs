using System;
using System.Collections.Generic;

namespace E_LearningApp_WEBAPI.Models;

public partial class Enrollment
{
    public int EnrollmentId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public DateTime EnrollmentDate { get; set; }

    public decimal Progress { get; set; }

    public DateTime? CompletionDate { get; set; }

    public virtual Course Course { get; set; } = null!;
}
