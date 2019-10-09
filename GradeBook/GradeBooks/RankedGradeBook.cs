using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work.");

            int threshold = (int)Math.Ceiling(Students.Count * 0.2);
            List<double> grades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade)
                .ToList();

            int numberOfGrades = grades.FindAll(g => g >= averageGrade).Count;
            if (numberOfGrades <= threshold)
            {
                return 'A';
            }
            if (numberOfGrades <= threshold * 2)
            {
                return 'B';
            }
            if (numberOfGrades <= threshold * 3)
            {
                return 'C';
            }
            return numberOfGrades <= threshold * 4 ? 'D' : 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");

            base.CalculateStudentStatistics(name);
        }
    }
}
