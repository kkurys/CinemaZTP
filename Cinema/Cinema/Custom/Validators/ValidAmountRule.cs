using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Cinema
{
    public class ValidAmountRule : ValidationRule
    {
        private decimal min = 0;
        private decimal max = Decimal.MaxValue;
        public decimal Min
        {
            get
            {
                return min;
            }
            set
            {
                min = value;
            }
        }
        public decimal Max
        {
            get
            {
                return max;
            }
            set
            {
                max = value;
            }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            decimal amount = 0;
            try
            {
                if (((string)value).Length > 0)
                {
                    amount = decimal.Parse((string)value, NumberStyles.Any, cultureInfo);
                }
            }
            catch
            {
                return new ValidationResult(false, "Illegal characters");
            }
            if (amount < min || amount > max)
            {
                return new ValidationResult(false, "Amount not between " + min + " and" + max);
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
