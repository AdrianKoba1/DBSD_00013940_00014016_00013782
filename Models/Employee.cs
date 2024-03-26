using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DBSD_00013940_00014016_00013782.Models
{
    public class Employee
    {
        public int? EmployeeId { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }


        [DisplayName("Last name")]
        public string LastName { get; set; }


        [DisplayName("Birth date")]
        public DateTime? BirthDate { get; set; }


        public string PhoneNumber { get; set; }

        public string Position { get; set; }

        public byte[] Photo { get; set; }

        [MinLength(3)]
        [MaxLength(100)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}