using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }

        /*
            The purpose of the exercise is to implement the following methods.
            Each method should contain C# code, which with the help of LINQ will perform queries described using SQL.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Task1()
        {
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };

            Console.WriteLine("Task1, res:");
            foreach (var v in res)
            {
                Console.WriteLine($"v.Naswisko = {v.Nazwisko}, v.Zawod = {v.Zawod}");                
            }

            //2. Lambda and Extension methods
            var res2 = Emps.Where(e => e.Job == "Backend programmer");

            printTaskResult(res.ToArray(), 1);
            //Console.WriteLine("Task1, res2:");
            //foreach (var v in res2)
            //{
            //    Console.WriteLine(v);
            //}
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Task2()
        {
            var res = 
                Emps.Where(e => e.Job == "Frontend programmer" && e.Salary > 1000)
                .OrderByDescending(e => e.Ename);

            printTaskResult(res.ToArray(), 2);
        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Task3()
        {
            var res = Emps.Max(e => e.Salary);
            printTaskResult(new Object[] { res }, 3);
        }

        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Task4()
        {
            var res = Emps.Where(e => e.Salary == Emps.Max(e1 => e1.Salary));
            printTaskResult(res.ToArray(), 4);
        }

        /// <summary>
        /// SELECT ename AS FirstName, job AS EmployeeJob FROM Emps;
        /// </summary>
        public void Task5()
        {
            var res = Emps.Select(
                e => new { 
                    FirstName = e.Ename,
                    EmployeeJob = e.Job
                });
            printTaskResult(res.ToArray(), 5);
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Result: Joining collections Emps and Depts.
        /// </summary>
        public void Task6()
        {
            var res = from emp in Emps
                      join dept in Depts
                      on emp.Deptno equals dept.Deptno
                      select new
                      {
                          EmpName = emp.Ename,
                          EmpJob = emp.Job,
                          Deptname = dept.Dname
                      };
            printTaskResult(res.ToArray(), 6);
        }

        /// <summary>
        /// SELECT Job AS EmployeeJob, COUNT(1) EmployeeNuber FROM Emps GROUP BY Job;
        /// </summary>
        public void Task7()
        {
            var res = Emps.Select(
                s => new { 
                    EmployeeJob = s.Job
                }).GroupBy(e => e);
            printTaskResult(res.ToArray(), 7);
        }

        /// <summary>
        /// Return value "true" if at least one of 
        /// the elements of collection works as "Backend programmer".
        /// </summary>
        public void Task8()
        {
            var res = (Emps.Select(e => e.Job == "Backend programmer").Count() > 0);
            printTaskResult(new Object[] { res }, 8);
        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Task9()
        {
            var res = Emps.Where(e => e.Job == "Frontend programmer")
                        .OrderByDescending(e => e.HireDate)
                        .FirstOrDefault();
            printTaskResult(new Object[] { res }, 9);
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "No value", null, null;
        /// </summary>
        public void Task10()
        {
            var unionList = new[] {
                new{
                    Ename = "No value",
                    Job = (string)null,
                    HireDate = (DateTime?)null
                }
                };
            var res = Emps.Select(
                e => new { 
                    e.Ename,
                    e.Job,
                    e.HireDate
                })
                .Union(unionList);

            printTaskResult(res.ToArray(), 10);
        }

        //Find the employee with the highest salary using the Aggregate () method
        public void Task11()
        {
            var res = Emps.Aggregate((e1, e2) => e1.Salary > e2.Salary ? e1 : e2);
            printTaskResult(new Object[] { res }, 11);
        }

        //Using the LINQ language and the SelectMany method, 
        //perform a CROSS JOIN join between collections Emps and Depts
        public void Task12()
        {
            
        }

        public void printTaskResult(Object[] res, int taskIndex)
        {
            Console.WriteLine($"\nTask{taskIndex}:");
            foreach (var v in res)
            {
                Console.WriteLine(v);
            }
        }
    }
}