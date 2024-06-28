using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample_App.Model
{
    // Student - класс, описыващий студента
    internal class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public int Rate { get; set; }
        public decimal? Grants { get; set; }

        public Student() { }

        public override string ToString()
        {
            return $"{Id} - {LastName} - {FirstName} - {Rate} - {Grants}";
        }
    }
}
