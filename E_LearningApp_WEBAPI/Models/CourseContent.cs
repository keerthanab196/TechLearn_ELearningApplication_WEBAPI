using System;
using System.Collections.Generic;

namespace E_LearningApp_WEBAPI.Models;

public partial class CourseContent
{
    public int ContentId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public string Filepath { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Course Course { get; set; } = null!;
}
