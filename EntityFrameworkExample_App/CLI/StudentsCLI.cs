using EntityFrameworkExample_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkExample_App.CLI
{
    internal class StudentsCLI : IDisposable
    {
        private readonly IStudentService _studentService;

        private delegate void CLIAction();

        private Dictionary<string, CLIAction> cliActions;

        public StudentsCLI(IStudentService studentService)
        {
            _studentService = studentService;
            // 
            cliActions = new Dictionary<string, CLIAction>()
            {
                {"1", GetAll },
                {"2", Get },
                {"3", Add },
                {"4", Delete },
                {"5", Update },
                {"6", Filter},
                {"7", Index},
                {"8", Exit },
            };
        }

        public void RunCLI()
        {
            while (true)
            {
                Console.WriteLine("1. GetAll");
                Console.WriteLine("2. Get");
                Console.WriteLine("3. Add");
                Console.WriteLine("4. Delete");
                Console.WriteLine("5. Update");
                Console.WriteLine("6. Filter Students By Name");
                Console.WriteLine("7. Index");
                Console.WriteLine("8. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine() ?? "";
                if (cliActions.ContainsKey(choice))
                {
                    cliActions[choice]();   // вызываем нужжное действие меню
                } else
                {
                    Console.WriteLine("unknown action");
                }
            }
        }

        // МЕТОДЫ CLI

        private void GetAll()
        {
            Console.WriteLine("All students: ");
            foreach (Student s in _studentService.GetAll())
            {
                Console.WriteLine(s);
            }
        }

        private void Get()
        {
            Console.Write("Enter id for get: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Student? student = _studentService.Get(id);
            if (student == null)
            {
                Console.WriteLine($"There are no student with id '{id}'");
            }
            else
            {
                Console.WriteLine(student);
            }
        }

        private void Add()
        {
            Console.Write("Enter lastname: ");
            string lastName = Console.ReadLine() ?? "";
            Console.Write("Enter firstname: ");
            string firstName = Console.ReadLine() ?? "";
            Console.Write("Enter rate: ");
            int rate = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter grants (press enter to set null): ");
            string? grantsStr = Console.ReadLine();
            decimal? grants = null;
            if (grantsStr != null && grantsStr != "")
            {
                grants = Convert.ToDecimal(grantsStr);
            }
            Student newStudent = new Student()
            {
                LastName = lastName,
                FirstName = firstName,
                Rate = rate,
                Grants = grants
            };
            _studentService.Add(newStudent);
            Console.WriteLine($"Added: {newStudent}");
        }

        private void Delete()
        {
            Console.Write("Enter id for delete: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Student? deleted = _studentService.Delete(id);
            if (deleted == null)
            {
                Console.WriteLine($"There are no student with id '{id}'");
            } else
            {
                Console.WriteLine($"Deleted: {deleted}");
            }
        }

        private void Update()
        {
            Console.Write("Enter id for update: ");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter lastname: ");
            string lastName = Console.ReadLine() ?? "";
            Console.Write("Enter firstname: ");
            string firstName = Console.ReadLine() ?? "";
            Console.Write("Enter rate: ");
            int rate = Convert.ToInt32(Console.ReadLine());
            Console.Write("Enter grants (press enter to set null): ");
            string? grantsStr = Console.ReadLine();
            decimal? grants = null;
            if (grantsStr != null && grantsStr != "")
            {
                grants = Convert.ToDecimal(grantsStr);
            }
            Student newStudent = new Student()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Rate = rate,
                Grants = grants,
            };
            Student? updated = _studentService.Update(newStudent);
            if (updated == null)
            {
                Console.WriteLine($"There are no student with id '{id}'");
            }
            else
            {
                Console.WriteLine($"Updated: {updated}");
            }
        }

        private void Filter()
        {
            Console.Write("Enter pattern to filter students by name: ");
            string pattern = Console.ReadLine() ?? "";
            List<Student> filteredStudents = _studentService.FilterStudentsByName(pattern);
            
            Console.WriteLine("Filtered students: ");
            foreach (Student s in filteredStudents)
            {
                Console.WriteLine(s);
            }
        }

        private void Index()
        {
            Console.WriteLine("Enter the percentage of scholarship increase and the minimum rating: ");
    
            string percentageStr = Console.ReadLine() ?? "";
            string minRatingStr = Console.ReadLine() ?? "";
            
            if (int.TryParse(percentageStr, out int percentage) && int.TryParse(minRatingStr, out int minRating))
            {
                _studentService.GrantsIndexation(percentage, minRating);
                Console.WriteLine("Scholarships have been indexed.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter valid numbers.");
            }
        }

        
        private void Exit()
        {
            Environment.Exit(0);
        }

        //
        public void Dispose()
        {
            _studentService.Dispose();
        }
    }
}
