﻿using System;
using System.Collections.Generic;

namespace E_LearningApp_WEBAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int? RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual UserRole Role { get; set; } = null!;
}
