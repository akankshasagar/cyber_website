﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberSecurity_new.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }


    public class Module
    {
        public int Id { get; set; }
        public string Module_Name { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Courses Courses { get; set; }
    }


    public class Topic
    {
        public int Id { get; set; }
        public string Topic_Name { get; set; }
        public string Topic_Description { get; set; }
        public string T_ImagePath { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public virtual Courses Courses { get; set; }
        public virtual Module Module { get; set; }

    }

    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }

        public string CorrectOption { get; set; }

        [ForeignKey("Module")]
        public int ModuleId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        // Navigation Property
        public virtual Module Module { get; set; }
    }

    public class Answer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; } // Foreign key to the Question table

        [ForeignKey("Module")]
        public int ModuleId { get; set; } // Foreign key to the Module table

        [ForeignKey("Courses")]
        public int CourseId { get; set; } // Foreign key to the Course table

        [Required]
        public string AnswerText { get; set; } // User's submitted answer
        public bool IsCorrect { get; set; } // Indicates whether the answer is correct
        public DateTime SubmittedAt { get; set; } // Timestamp of the answer submission
        public string SubmittedBy { get; set; } // User who submitted the answer

        // Navigation properties
        public virtual Question Question { get; set; }
        public virtual Module Module { get; set; }
        public virtual Courses Courses { get; set; }
    }

}
