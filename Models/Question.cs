﻿using System;
using System.Collections.Generic;

namespace QuizWinform.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public int? ExamId { get; set; }
        public string? QuestionText { get; set; }
        public string? OptionA { get; set; }
        public string? OptionB { get; set; }
        public string? OptionC { get; set; }
        public string? OptionD { get; set; }
        public string? CorrectAnswer { get; set; }

        public virtual Exam? Exam { get; set; }
    }
}
