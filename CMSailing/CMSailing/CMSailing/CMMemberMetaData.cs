/*CMMemberMetaData.cs
 * Validates and sets the data and field names
 * 
 * Revision History:
 *  Chris Mosey: 8.11.2012: created
 *  Chris Mosey: 12.11.2012: finished
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using CMSailing.App_GlobalResources;
using CMClassLibrary;

namespace CMSailing.Models
{
    [MetadataType(typeof(CMMemberMetaData))]
    public partial class member : IValidatableObject   
    {
        private sailContext db = new sailContext();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {           
            if (firstName == null || firstName.Trim() == "")
            {
                yield return new ValidationResult(string.Format(Tranlations.errorRequired, Tranlations.firstName), new []{"firstName"});
            }
            if(lastName == null || lastName.Trim() == "")
            {
                yield return new ValidationResult(string.Format(Tranlations.errorRequired, Tranlations.lastName), new[] { "lastName" });
            }
            if ((spouseLastName != null && spouseLastName.Trim() != "") && (spouseFirstName == null || spouseFirstName.Trim() == ""))
            {
                yield return new ValidationResult(Tranlations.errorSpouseLastName, new[] { "spouseFirstName" });
            }

            if (spouseFirstName!= null && spouseFirstName.Trim() != "")
            {
                if (spouseLastName == lastName)
                {
                    fullName = lastName + ", " + firstName + " & " + spouseFirstName;
                }
                else
                {
                    fullName = lastName + ", " + firstName + " & " + spouseLastName + ", " + spouseFirstName;
                }
            }
            else
            {
                fullName = lastName + ", " + firstName;
            }

            if (homePhone != null && homePhone.Trim() != "")
            {
                Regex reg = new Regex(@"\d{3}-\d{3}-\d{4}");
                if(!reg.Match(homePhone).Success)
                {
                    yield return new ValidationResult(Tranlations.errorPhone, new[] { "homePhone" });
                }
            }
            if(useCanadaPost == true)
            {
                if (street == null || street.Trim() == "")
                {
                    yield return new ValidationResult(string.Format(Tranlations.errorRequired, Tranlations.street), new[] { "street" });
                }
                if (city == null || city.Trim() == "")
                {
                    yield return new ValidationResult(string.Format(Tranlations.errorRequired, Tranlations.city), new[] { "city" });
                }
            }
            else
            {
                if ((email == null || email.Trim() == ""))
                {
                    yield return new ValidationResult(string.Format(Tranlations.errorRequired, Tranlations.email), new[] { "email" });
                }
            }
            if (email != null && email.Trim() != "")
            {
                Regex reg = new Regex(@"^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2, 4}&", RegexOptions.IgnoreCase);
                if(!reg.Match(email).Success)
                {
                    yield return new ValidationResult(Tranlations.errorEmail, new[] { "email" });
                }
            }

            string provCode = "";
            try
            {
                var provCodeDB = db.provinces.Find(provinceCode).provinceCode;
                provCode = provCodeDB;
            }
            catch
            { }

            if (provinceCode == null || provinceCode.Trim() == "")
            {
                yield return new ValidationResult(string.Format(Tranlations.errorRequired, Tranlations.provinceCode), new[] { "provinceCode" });
            }
            else if (provinceCode.Length > 2)
            {
                yield return new ValidationResult(Tranlations.errorProvinceCode, new[] { "provinceCode" });
            }
            else if (provinceCode != provCode)
            {
                yield return new ValidationResult(Tranlations.errorProvinceCodeNOF, new[] { "provinceCode" });
            }

            if (postalCode == null)
            {
                yield return new ValidationResult(Tranlations.postalCode, new[] { "postalCode" });
            }
            
            postalCode = postalCode.ToUpper();

        }
    }
    public class CMMemberMetaData
    {
        [Display(Name="memberId", ResourceType=typeof(Tranlations))]
        public int memberId { get; set; }

        [Display(Name = "fullName", ResourceType = typeof(Tranlations))]
        public string fullName { get; set; }

        [Display(Name = "firstName", ResourceType = typeof(Tranlations))]
        public string firstName { get; set; }

        [Display(Name = "lastName", ResourceType = typeof(Tranlations))]
        public string lastName { get; set; }

        [Display(Name = "spouseFirstName", ResourceType = typeof(Tranlations))]
        public string spouseFirstName { get; set; }

        [Display(Name = "spouseLastName", ResourceType = typeof(Tranlations))]
        public string spouseLastName { get; set; }

        [Display(Name = "street", ResourceType = typeof(Tranlations))]
        public string street { get; set; }

        [Display(Name = "city", ResourceType = typeof(Tranlations))]
        public string city { get; set; }

        [Display(Name = "provinceCode", ResourceType = typeof(Tranlations))]
        public string provinceCode { get; set; }

        [CMPostalCodeValidation]
        [Display(Name = "postalCode", ResourceType = typeof(Tranlations))]
        public string postalCode { get; set; }

        [Display(Name = "homePhone", ResourceType = typeof(Tranlations))]
        public string homePhone { get; set; }

        [Display(Name = "email", ResourceType = typeof(Tranlations))]
        public string email { get; set; }

        [Display(Name = "yearJoined", ResourceType = typeof(Tranlations))]
        public Nullable<int> yearJoined { get; set; }

        [Display(Name = "comment", ResourceType = typeof(Tranlations))]
        public string comment { get; set; }

        [Display(Name = "taskExempt", ResourceType = typeof(Tranlations))]
        public Nullable<bool> taskExempt { get; set; }

        [Display(Name = "useCanadaPost", ResourceType = typeof(Tranlations))]
        public Nullable<bool> useCanadaPost { get; set; }
    }
}