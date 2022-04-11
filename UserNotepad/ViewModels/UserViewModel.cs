using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Extensions.Attributes;
using UserNotepad.Models;

namespace UserNotepad.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(150)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [NotFeatureDate]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Required]
        public Gender? Gender { get; set; }

        [EmailAddress]
        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public PhoneNumberViewModel PhoneNumber { get; set; }

        public int Age { get; set; }

        public string Title { get; set; }
    }
}
