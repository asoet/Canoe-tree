using System.ComponentModel.DataAnnotations;

namespace PlantHunter.Mobile.Core.Validations
{
    public class ValidUrlRule : IValidationRule<string>
    {
        public ValidUrlRule() => ValidationMessage = "Should be an URL";

        public string ValidationMessage { get; set; }

        public bool Check(string value) => new UrlAttribute().IsValid(value);
    }
}
