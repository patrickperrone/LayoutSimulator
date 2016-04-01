using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sitecore.Feature.LayoutSimulator.Tests.Models
{
    public abstract class ModelTests
    {
        protected bool IsModelValid(object model)
        {
            return ValidateModel(model).Count == 0;
        }

        protected IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}
