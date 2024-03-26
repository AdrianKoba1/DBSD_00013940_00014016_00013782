using DBSD_00013940_00014016_00013782.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DBSD_00013940_00014016_00013782.DAL
{
    public class EmployeeRepository: IEmployeeRepository
    {

        private string ConnString => WebConfigurationManager.ConnectionStrings["SoyConString"].ConnectionString;

        //Get All employees method
        public IList<Employee> GetAll()
        {
             IList<Employee> employees = new List<Employee>();
             using(var conn = new SqlConnection(ConnString))
             {
                using (var cmd = conn.CreateCommand()) {
                    cmd.CommandText = @"SELECT [EmployeeID]
                                             ,[FirstName]
                                             ,[LastName]
                                             ,[BirthDate]
                                             ,[PhoneNumber]
                                             ,[Position]
                                             ,[Email]
                                            FROM [dbo].[Employee]
                                             ";

                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var emp = new Employee();

                            emp.EmployeeId = rdr.GetInt32(rdr.GetOrdinal("EmployeeID"));
                            emp.FirstName = rdr.GetString(rdr.GetOrdinal("FirstName"));
                            emp.LastName = rdr.GetString(rdr.GetOrdinal("LastName"));
                            if (!rdr.IsDBNull(rdr.GetOrdinal("BirthDate")))
                                emp.BirthDate = rdr.GetDateTime(rdr.GetOrdinal("BirthDate"));

                            if (!rdr.IsDBNull(rdr.GetOrdinal("PhoneNumber")))
                                emp.PhoneNumber = rdr.GetString(rdr.GetOrdinal("PhoneNumber"));

                            emp.Position = rdr.GetString(rdr.GetOrdinal("Position"));

                            emp.Email = rdr.GetString(rdr.GetOrdinal("Email"));

                            employees.Add(emp);
                        }


                    }
                }
             }
             return employees;
        }


        //Insert new employee method

        public void Insert(Employee emp)
        {
            using (var conn = new SqlConnection(ConnString))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [dbo].[Employee]
                                        ([FirstName]
                                         ,[LastName]
                                         ,[BirthDate]
                                         ,[PhoneNumber]
                                         ,[Position]
                                         ,[Photo]
                                         ,[Email]
                                         ,[Password]
                                          )
                                   VALUES
                                               (
                                                 @FirstName
                                                ,@LastName
                                                ,@BirthDate
                                                ,@PhoneNumber
                                                ,@Position
                                                ,@Photo
                                                ,@Email
                                                ,convert(varchar(256), hashbytes('SHA2_256', @Password) ,2)
                                        )";

                    var pFirstName = cmd.CreateParameter();
                    pFirstName.ParameterName = "@FirstName";
                    pFirstName.Value = emp.FirstName;
                    pFirstName.Direction = ParameterDirection.Input;
                    pFirstName.DbType = DbType.AnsiString;
                    cmd.Parameters.Add(pFirstName);

                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@BirthDate", (object)emp.BirthDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhoneNumber", emp.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Position", emp.Position);

                    cmd.Parameters.AddWithValue("@Photo", (object)emp.Photo ?? SqlBinary.Null);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Password", emp.Password);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}