using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.ViewModels;

namespace UserNotepad.Services
{
    public interface IPhoneNumberService
    {
        IEnumerable<SelectListItem> GetCountryCodes();
        PhoneNumberViewModel GetPhoneNumber();
        PhoneNumberViewModel GetPhoneNumber(string phoneNumber);
        bool IsValidPhoneNumber(string phoneNumber);
    }
}
