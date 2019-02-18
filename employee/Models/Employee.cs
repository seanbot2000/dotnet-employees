using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace employee.Models{

    [Table("employee")]
    public class Employee
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid id { get; set; }

        public string firstName { get; set;}
        public string lastName { get; set;}

        public string email { get; set;}
        
    }
}