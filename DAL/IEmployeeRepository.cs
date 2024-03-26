using DBSD_00013940_00014016_00013782.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBSD_00013940_00014016_00013782.DAL
{
    internal interface IEmployeeRepository
    {
        IList<Employee> GetAll();
        void Insert(Employee emp);
    }
}
