using EntityFrameworkExample_App.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample_App.Rdb
{
    // RdbStudent - сущность студента, отображаемая в БД
    [Table("students_t")]
    internal class RdbStudent
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("last_name_f")]
        public string LastName { get; set; } = string.Empty;

        [Column("first_name_f")]
        public string FirstName { get; set; } = string.Empty;

        [Column("rate_f")]
        public int Rate { get; set; }

        [Column("grants_f")]
        public decimal? Grants { get; set; }

        public RdbStudent() { }

        // МЕТОДЫ МАППИНГА МЕЖДУ Student и RdbStudent
        public static Student MapToStudent(RdbStudent rdbStudent)
        {
            return new Student()
            {
                Id = rdbStudent.Id,
                LastName = rdbStudent.LastName,
                FirstName = rdbStudent.FirstName,
                Rate = rdbStudent.Rate,
                Grants = rdbStudent.Grants
            };
        }

        public static RdbStudent MapFromStudent(Student student)
        {
            return new RdbStudent()
            {
                Id = student.Id,
                LastName = student.LastName,
                FirstName = student.FirstName,
                Rate = student.Rate,
                Grants = student.Grants
            };
        }

        public override string ToString()
        {
            return $"{Id} - {LastName} - {FirstName} - {Rate} - {Grants}";
        }
    }
}
