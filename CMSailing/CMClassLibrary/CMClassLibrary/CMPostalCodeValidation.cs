/*CMPostalValidation.cs
 * Validates postal code using regular expressions
 * 
 * Revision History:
 *  Chris Mosey: 12.11.2012: created
 *  Chris Mosey: 12.11.2012: finished
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CMClassLibrary
{
    public class CMPostalCodeValidation : ValidationAttribute
    {
        public CMPostalCodeValidation() : base("PostalCode Invalid, ex: n1n 1n1") { }

        protected override ValidationResult IsValid (object value, ValidationContext validationContext)
        {
            Regex reg = new Regex(@"^[A-Z]\d[A-Z] ?\d[A-Z]\d$", RegexOptions.IgnoreCase);

            if (value != null)
            {
                string PC = value.ToString().ToUpper();

                if (PC.Trim() == "")
                {
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
                else
                {
                    if (!reg.Match(PC).Success)
                    {
                        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
