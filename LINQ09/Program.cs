using System;

namespace LINQ09.Shared
{
    internal class Program
    {
        public static void Main(String[] args)
        {
            RunGroupJoin();
            Console.ReadKey();

        }
        private static void RunJoin()
        {
            var employees = Repository.LoadEmployees();
            var department = Repository.LoadDepartment();

            var result = employees.Join(department, emp => emp.DepartmentId, dept => dept.Id,
                (emp, dept) => new
                {
                    FullName = emp.FullName,
                    Department = dept.Name
                });
            foreach (var item in result)
            {
                Console.WriteLine($"{item.FullName} [{item.Department}]");
            }
        }
        private static void RunJoinQuerySyntax()
        {
            var employees = Repository.LoadEmployees();
            var department = Repository.LoadDepartment();

            var result = from emp in employees
                         join dept in department on emp.DepartmentId equals dept.Id
                         select new EmployeeDto
                         {
                             FullName = emp.FullName,
                             Department = dept.Name
                         };

            foreach (var item in result)
            {
                Console.WriteLine($"{item.FullName} [{item.Department}]");
            }
        }
        private static void RunGroupJoin()
        {
            var employees = Repository.LoadEmployees();
            var department = Repository.LoadDepartment();
            var result = department.GroupJoin(
                employees,
                dept => dept.Id,
                emp => emp.DepartmentId,
                (dept, emps) => new Group
                {
                    Department = dept.Name,
                    Employees = emps.Select(e => e.FullName).ToList()
                }
                );


            foreach (var g in result)
            {
                Console.WriteLine($"+++++++++ {g.Department} ++++++++++");
                foreach(var name in g.Employees)
                {
                    Console.WriteLine($"\t{name}");
                }
            }
        }


    }
}