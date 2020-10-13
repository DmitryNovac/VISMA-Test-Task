using System;

namespace VISMA.TestTask.Data.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Employee")]
    public class Employer
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
