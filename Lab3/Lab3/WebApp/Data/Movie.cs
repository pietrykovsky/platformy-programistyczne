using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApp.Data;

    public class CustomDateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _minDate;
        private DateTime? _maxDate;

        public CustomDateRangeAttribute(string minDate)
        {
            _minDate = DateTime.Parse(minDate, CultureInfo.InvariantCulture);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime dt = (DateTime)value;
            _maxDate = DateTime.Now.AddYears(5); // Dynamically set the max date

            if (dt < _minDate || dt > _maxDate)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"The date must be between {_minDate.ToString("d", CultureInfo.InvariantCulture)} and {_maxDate?.ToString("d", CultureInfo.InvariantCulture)}.";
        }
    }

public class Movie
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    [CustomDateRange("1/1/1900")]
    public DateTime ReleaseDate { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    [Range(1, 10)]
    public float Rating { get; set; }
}
