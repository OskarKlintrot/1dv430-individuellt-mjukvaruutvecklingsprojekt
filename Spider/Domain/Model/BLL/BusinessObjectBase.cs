using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.BLL
{
    public class BusinessObjectBase
    {
        public bool Validate(out ICollection<ValidationResult> validationResults)
        {
            var validationContext = new ValidationContext(this);
            validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(this, validationContext, validationResults, true);
        }
    }
}
