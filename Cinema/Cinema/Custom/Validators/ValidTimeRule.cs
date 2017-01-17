using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cinema
{
    public class ValidTimeRule : ValidationRule
    {
      
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string time = value as string;

            if (Regex.IsMatch(time, @"^[012]{1}[0-9]{1}:[012345]{1}[0123456789]{1}$"))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Illegal characters!");
            }

        }
    }
}
