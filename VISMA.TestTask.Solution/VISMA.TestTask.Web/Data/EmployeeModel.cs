using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using VISMA.TestTask.Data.Models;

namespace VISMA.TestTask.Web.Data
{
    public class EmployeeModel
    {
        [Required]
        [DisplayName("First Name")]
        [StringLength(maximumLength: 25)]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [StringLength(maximumLength: 25)]
        public string LastName { get; set; }

        [Required]
        [DisplayName("Social Security Number")]
        [StringLength(maximumLength: 25)]
        public string SocialSecurityNumber { get; set; }

        [DisplayName("Phone Number")]
        [RegularExpression("^[+]?([(][0-9 ]{1,5}[)])?[0-9 ]{3,}$", ErrorMessage = "Invalid Phone number")]
        [StringLength(maximumLength: 25)]
        public string PhoneNumber { get; set; }

        public Employee ToEntity()
        {
            return new Employee
            {
                FirstName = FirstName,
                LastName = LastName,
                SocialSecurityNumber = SocialSecurityNumber,
                PhoneNumber = PhoneNumber,
            };
        }
    }
}
