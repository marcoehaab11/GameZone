namespace GameZone.Attributes
{
    public class MaxFileSizaAttribute: ValidationAttribute

    {
        private readonly int _maxFileSiza;
        public MaxFileSizaAttribute(int allowedExtensions)
        {
            _maxFileSiza = allowedExtensions;
        }
        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                if (file.Length>_maxFileSiza)
                {
                    return new ValidationResult($"Maximum allowed size is {_maxFileSiza} bytes");
                }

            }
            return ValidationResult.Success;

        }
    }
}

