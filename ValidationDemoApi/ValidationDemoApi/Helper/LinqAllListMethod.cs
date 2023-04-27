using System.Data;
using ValidationDemoApi.Entities;
using ValidationDemoApi.Models;

namespace ValidationDemoApi.Helper
{
    public class LinqAllListMethod
    {
        private readonly JWTokenDBContext _db;

        public LinqAllListMethod(JWTokenDBContext db)
        {
            _db = db;
        }
        public Response SelectQueryList()
        {
            Response response = new Response();
            try
            {
                #region //comapre Id
                List<DepartmentClass> departments = new List<DepartmentClass>();
                departments.Add(new DepartmentClass { DepartmentId = 1, Name = "Account" });
                departments.Add(new DepartmentClass { DepartmentId = 2, Name = "Sales" });
                departments.Add(new DepartmentClass { DepartmentId = 3, Name = "Marketing" });

                List<EmployeeClass> employees = new List<EmployeeClass>();
                employees.Add(new EmployeeClass { DepartmentId = 1, EmployeeId = 1, EmployeeName = "William" });
                employees.Add(new EmployeeClass { DepartmentId = 2, EmployeeId = 2, EmployeeName = "Miley" });
                employees.Add(new EmployeeClass { DepartmentId = 1, EmployeeId = 3, EmployeeName = "Benjamin" });


                var list = (from e in employees
                            join d in departments on e.DepartmentId equals d.DepartmentId
                            select new
                            {
                                EmployeeName = e.EmployeeName,
                                DepartmentName = d.Name
                            });

                foreach (var e in list)
                {
                    var res = e.EmployeeName;
                }
                #endregion


                #region//Substring
                List<string> words = new List<string>() { "an", "apple", "a", "day" };
                var query = from word in words select word.Substring(0, 2); // all list value of 0 to 2 number list
                foreach (string s in query)
                    #endregion

                    response.Data = s;
                response.Data = list;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public Response LinqdataList()
        {
            Response response = new Response();
            try
            {

                #region // line to convert word
                //List<string> phrases = new List<string>() { "an apple a day", "the quick brown fox" };

                //var query = from phrase in phrases
                //            from word in phrase.Split(' ') 
                //            select word;
                //#endregion

                //#region // word lenth to found word 
                //string[] words = { "humpty", "dumpty", "set", "on", "a", "wall" };
                //IEnumerable<string> query1 = from word in words where word.Length == 3 select word;
                //#endregion

                //#region //how to get odd and even number 
                //List<int> numbers = new List<int>() { 35, 44, 200, 84, 3987, 4, 199, 329, 446, 208 };

                //IEnumerable<IGrouping<int,int>> query2 = from number in numbers
                //                                         group number by number % 2;
                #endregion

                #region // id match retrun data
                var query = _db.Students.Join(_db.TblUserTokenMsts, customer => customer.RollNo,
                        invoice => invoice.Id, (customer, invoice) => new
                        {
                            InvoiceID = invoice.Id,
                            CustomerName = customer.Name + "" + customer.Address,
                            InvoiceDate = invoice.TokenCreated
                        }
                    ).ToList();
                #endregion

                response.Data = query;

            }
            catch (Exception ex)
            {

            }
            return response;
        }


        public Response JoindataList()
        {
            Response response = new Response();

            try
            {
                #region get teenager list
                IList<Student> studentList = new List<Student>()
                {
                    new Student() { RollNo = 1, Name = "John", Age = 13} ,
                    new Student() { RollNo = 2, Name = "Moin",  Age = 21 } ,
                    new Student() { RollNo = 3, Name = "Bill",  Age = 18 } ,
                    new Student() { RollNo = 4, Name = "Ram" , Age = 20} ,
                    new Student() { RollNo = 5, Name = "Ron" , Age = 15 }
                };

                Func<Student, bool> isTeenAger = delegate (Student s)
                {
                    return s.Age > 12 && s.Age < 20;
                };

                var filteredResult = from s in studentList
                                     where isTeenAger(s)
                                     select s;
                #endregion


                #region
                IList<string> stringList = new List<string>() {
                        "C# Tutorials",
                        "VB.NET Tutorials",
                        "Learn C++",
                                "MVC Tutorials" ,
                                "Java"
                            };

                            // LINQ Query Syntax
                            var result = from s in stringList
                                         where s.Contains("Tutorials")
                                         select s;



                #endregion

                response.Data = result;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public static string DataTableLinq(DataTable dataTable)
        {
            if (dataTable == null)
            {
                return string.Empty;
            }

            return "abc";
            //return "["
            //        + string.Join(",", dataTable.Rows.OfType()
            //        .Select(row =>
            //            "{"
            //            + string.Join(",", dataTable.Columns.OfType()
            //                .Select(col => string.Format("\"{0}\":\"{1}\"",
            //                                    col.ColumnName,
            //                                    row[col].ToString())))
            //            + "}"))
            //        + "]";
        }


    }
}
