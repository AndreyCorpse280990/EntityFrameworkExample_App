using EntityFrameworkExample_App.CLI;
using EntityFrameworkExample_App.Rdb;

using (ApplicationDbContext db = new ApplicationDbContext())
using (RdbStudentService rdbStudentService = new RdbStudentService(db))
using (StudentsCLI cli = new StudentsCLI(rdbStudentService))
{
    cli.RunCLI();
}
