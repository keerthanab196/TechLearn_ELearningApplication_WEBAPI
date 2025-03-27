using System;
using System.Collections.Generic;

namespace E_LearningApp_WEBAPI.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int UserId { get; set; }

    public int CourseId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
