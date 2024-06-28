using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample_App.Model
{
    // IStudentService - интерфейс сервиса для работы со студентами
    internal interface IStudentService : IDisposable
    {
        // Add - добавить нового студента
        void Add(Student student);

        // GetAll - получить список всех студентов
        List<Student> GetAll();

        // Get - получение студента по id
        Student? Get(int id);

        // Update - обновить данные студента
        Student? Update(Student student);

        // Delete - удаление студента по id
        Student? Delete(int id);

        // FilterStudentsByName - получение списка студентов, имя или фамилия которых включает в себя 
        // переданную подстроку с игнорированием регистра
        List<Student> FilterStudentsByName(string pattern);

        // GrantsIndexation - индексация стипендии
        // всем студентам со стипендией и рейтингом >= minRating увеличить стипендию на percantage процентов
        void GrantsIndexation(int percantage, int minRating);
    }
}
