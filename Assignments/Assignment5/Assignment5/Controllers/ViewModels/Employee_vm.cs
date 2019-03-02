using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment5.Controllers
{
    public class EmployeeAdd
    {
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        public int? ReportsTo { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        public string Phone { get; set; }

        [StringLength(24)]
        public string Fax { get; set; }

        [StringLength(60)]
        public string Email { get; set; }
    }

    public class EmployeeBase : EmployeeAdd
    {
        [Key]
        public int Id { get; set; }
    }

    public class EmployeeEditProfileInfoForm
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string Title { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        // Adds <input type="tel> attribute
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\+?[ ]*[1-9]?[ ]*\-?[ ]*\(?[ ]*[1-9][ ]*(\d[ ]*){2}\)?[ ]*\-?[ ]*(\d[ ]*){3}-[ ]*(\d[ ]*){4}",
            ErrorMessage = "Invalid Phone number")]
        public string Phone { get; set; }

        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"\+?[ ]*[1-9]?[ ]*\-?[ ]*\(?[ ]*[1-9][ ]*(\d[ ]*){2}\)?[ ]*\-?[ ]*(\d[ ]*){3}-[ ]*(\d[ ]*){4}",
            ErrorMessage = "Invalid Fax number")]
        public string Fax { get; set; }

        [StringLength(60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class EmployeeEditProfileInfo
    {
        [Key]
        public int Id { get; set; }

        [StringLength(70)]
        public string Address { get; set; }

        [StringLength(40)]
        public string City { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(40)]
        public string Country { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        [StringLength(24)]
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone number")]
        [RegularExpression(@"\+?[ ]*[1-9]?[ ]*\-?[ ]*\(?[ ]*[1-9][ ]*(\d[ ]*){2}\)?[ ]*\-?[ ]*(\d[ ]*){3}-[ ]*(\d[ ]*){4}",
            ErrorMessage = "Invalid Phone number")]
        public string Phone { get; set; }

        [StringLength(24)]
        // Specifies that a data field value is a well-formed phone number.
        // Does NOT validate the phone number
        //public class DataTypeAttribute : ValidationAttribute
        //
        // Summary:
        //     Checks that the value of the data field is valid.
        //
        // Parameters:
        //   value:
        //     The data field value to validate.
        //
        // Returns:
        //     true always.
        //public override bool IsValid(object value);
        //[DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Fax number")]
        [RegularExpression(@"\+?[ ]*[1-9]?[ ]*\-?[ ]*\(?[ ]*[1-9][ ]*(\d[ ]*){2}\)?[ ]*\-?[ ]*(\d[ ]*){3}-[ ]*(\d[ ]*){4}",
            ErrorMessage = "Invalid Fax number")]
        public string Fax { get; set; }

        [StringLength(60)]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid Email address")]
        public string Email { get; set; }
    }
}