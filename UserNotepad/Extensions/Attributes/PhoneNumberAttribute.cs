using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Services;

namespace UserNotepad.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PhoneNumberAttribute : ValidationAttribute
    {
        private const string defaultErrorMessage = "The {0} field is not a valid phone number.";

        public PhoneNumberAttribute(string countryCodeProperty = null) : base(defaultErrorMessage)
        {
            CountryCodeProperty = countryCodeProperty;
        }

        public string CountryCodeProperty { get; private set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string nationalSignificantNumber = value != null ? value.ToString() : string.Empty;

            if (string.IsNullOrWhiteSpace(nationalSignificantNumber))
            {
                return ValidationResult.Success;
            }

            string countryCode = string.Empty;

            if (CountryCodeProperty != null)
            {
                var properties = TypeDescriptor.GetProperties(validationContext.ObjectInstance);
                var countryCodeProperty = properties.Find(CountryCodeProperty, false);

                if (countryCodeProperty != null)
                {
                    object countryCodeValue = countryCodeProperty.GetValue(validationContext.ObjectInstance);

                    if (countryCodeValue != null)
                    {
                        countryCode = countryCodeValue.ToString();

                        if (countryCode.Substring(0, 1) != "+")
                        {
                            countryCode = "+" + countryCode;
                        }
                    }
                }
            }

            var phoneNumberService = (IPhoneNumberService)validationContext.GetService(typeof(IPhoneNumberService));

            string phoneNumber = countryCode + nationalSignificantNumber;

            if (phoneNumberService.IsValidPhoneNumber(phoneNumber))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}
