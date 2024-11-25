using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ABC.CustomValidation
{
    public class PasswordComplexityAttribute: ValidationAttribute
    {
        private readonly int _minLength;
        private readonly string _specialCharacters;

        public PasswordComplexityAttribute(int minLength = 7, string specialCharacters = "%$#@!^&*")
        {
            _minLength = minLength;
            _specialCharacters = specialCharacters;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
            {
                return new ValidationResult("Password is required.");
            }

            // Check minimum length
            if (password.Length < _minLength)
            {
                return new ValidationResult($"Password must be at least {_minLength} characters long.");
            }

            // Check for special characters
            var specialCharPattern = $"[{Regex.Escape(_specialCharacters)}]";
            if (!Regex.IsMatch(password, specialCharPattern))
            {
                return new ValidationResult($"Password must contain at least one special character ({_specialCharacters}).");
            }

            return ValidationResult.Success;
        }
    }
}
