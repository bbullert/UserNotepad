using Microsoft.AspNetCore.Mvc.Rendering;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.ViewModels;

namespace UserNotepad.Services
{
    public class PhoneNumberService : IPhoneNumberService
    {
        public bool IsValidPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(phoneNumber))
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();

                try
                {
                    var numberProto = phoneUtil.Parse(phoneNumber, null);

                    if (phoneUtil.IsPossibleNumber(numberProto) && phoneUtil.IsValidNumber(numberProto))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        public PhoneNumberViewModel GetPhoneNumber(string phoneNumber)
        {
            PhoneNumberViewModel model;

            if (IsValidPhoneNumber(phoneNumber))
            {
                var phoneUtil = PhoneNumberUtil.GetInstance();
                var numberProto = phoneUtil.Parse(phoneNumber, null);

                model = new PhoneNumberViewModel
                {
                    NationalNumber = numberProto.NationalNumber.ToString(),
                    FormattedNationalNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.NATIONAL),
                    FormattedInternationalNumber = phoneUtil.Format(numberProto, PhoneNumberFormat.INTERNATIONAL),
                    CountryCode = numberProto.CountryCode.ToString()
                };
            }
            else
            {
                model = new PhoneNumberViewModel();
            }

            model.CountryCodeOptions = GetCountryCodes().ToList();

            return model;
        }

        public PhoneNumberViewModel GetPhoneNumber()
        {
            PhoneNumberViewModel model = new PhoneNumberViewModel
            {
                CountryCodeOptions = GetCountryCodes().ToList()
            };

            return model;
        }

        public IEnumerable<SelectListItem> GetCountryCodes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "PL (+48)", Value = "48" },
                new SelectListItem { Text = "UK (+44)", Value = "44" },
                new SelectListItem { Text = "USA (+1)", Value = "1" }
            };
        }
    }
}
