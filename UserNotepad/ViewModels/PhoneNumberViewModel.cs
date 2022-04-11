using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Extensions.Attributes;

namespace UserNotepad.ViewModels
{
    public class PhoneNumberViewModel
    {
        [PhoneNumber("CountryCode")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string NationalNumber { get; set; }

        public string FormattedNationalNumber { get; set; }

        public string InternationalNumber => $"+{CountryCode}{NationalNumber}";

        public string FormattedInternationalNumber { get; set; }

        public string CountryCode { get; set; }

        public List<SelectListItem> CountryCodeOptions { get; set; }
    }
}
