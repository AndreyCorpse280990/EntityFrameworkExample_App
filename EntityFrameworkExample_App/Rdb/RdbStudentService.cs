using EntityFrameworkExample_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample_App.Rdb
{
    internal class RdbStudentService : IStudentService
    {
        // объект для работы с БД через Entity Framework
        private readonly ApplicationDbContext _db;

        public RdbStudentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Student> GetAll()
        {
            return _db.Students
                .Select(rdbStudent => RdbStudent.MapToStudent(rdbStudent))
                .ToList();
        }

        public Student? Get(int id)
        {
            RdbStudent? rdbStudent = _db.Students.FirstOrDefault(rdbStudent => rdbStudent.Id == id);
            if (rdbStudent == null)
            {
                return null;
            }
            return RdbStudent.MapToStudent(rdbStudent);
        }

        public void Add(Student student)
        {
            // 1. получить rdb-сущность
            RdbStudent rdbStudent = RdbStudent.MapFromStudent(student);
            // 2. добавить в таблицу студентов на стороне приложения
            _db.Students.Add(rdbStudent);
            // 3. зафиксировать изменения в БД на постоянной основе
            _db.SaveChanges();    // в этот момент в объект rdbStudent запишется id
            student.Id = rdbStudent.Id;
        }

        public Student? Delete(int id)
        {
            RdbStudent? deleted = _db.Students.FirstOrDefault(s => s.Id == id);
            if (deleted == null)
            {
                return null;
            }
            _db.Students.Remove(deleted);
            _db.SaveChanges();
            return RdbStudent.MapToStudent(deleted);
        }

        public Student? Update(Student student)
        {
            RdbStudent? updated = _db.Students.FirstOrDefault(s => s.Id == student.Id);
            if (updated == null)
            {
                return null;
            }
            // поля переданного объекта заносятся в БД и фиксируются там
            updated.FirstName = student.FirstName;
            updated.LastName = student.LastName;
            updated.Rate = student.Rate;
            updated.Grants = student.Grants;
            _db.SaveChanges();
            return RdbStudent.MapToStudent(updated);
        }

        public List<Student> FilterStudentsByName(string pattern)
        {
            pattern = pattern.ToLower();
    
            return _db.Students
                .Where(s => s.LastName.ToLower().Contains(pattern) || s.FirstName.ToLower().Contains(pattern))
                .Select(rdbStudent => RdbStudent.MapToStudent(rdbStudent))
                .ToList();
        }

        public void GrantsIndexation(int percantage, int minRating)
        {
            if (percantage <= 0)
            {
                throw new ArgumentException("Percentage must be positive");
            }
            
            List<RdbStudent> studentToUpdate = _db.Students
                .Where(s => s.Rate >= minRating && s.Grants.HasValue)
                .ToList();

            foreach (var student in studentToUpdate)
            {
                student.Grants += student.Grants * percantage / 100;
            }

            _db.SaveChanges();
        }

        //
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
