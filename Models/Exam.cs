using System;
using System.Collections.Generic;

namespace QuizWinform.Models
{
    public partial class Exam
    {
        public Exam()
        {
            Questions = new HashSet<Question>();
        }

        public int ExamId { get; set; }
        public string? ExamName { get; set; }
        public int? TotalMarks { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
