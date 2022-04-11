using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotepad.Extensions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotFeatureDateAttribute : ValidationAttribute
    {
        private const string defaultErrorMessage = "The {0} field must be today or in the past.";

        public NotFeatureDateAttribute() : base(defaultErrorMessage)
        {

        }

        public override bool IsValid(object value)
        {
            DateTime date;
            if (DateTime.TryParse(value.ToString(), out date))
            {
                if (DateTime.Now >= date)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
