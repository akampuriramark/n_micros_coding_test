using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspCoreMVC.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("First Name")]
        [Required]
        public string FName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LName  { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Address { get; set; }
        [DisplayName("Birth Place")]
        [Required]
        public string BirthPlace { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
    }
}
