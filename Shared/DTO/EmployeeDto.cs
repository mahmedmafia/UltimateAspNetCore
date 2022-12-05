using System.ComponentModel.DataAnnotations;

namespace Shared.DTO
{
    public record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "Employee name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string? Name { get; init; }
        [Required(ErrorMessage = "Age is a required field.")]
        [Range(18, 60, ErrorMessage = "Age Must Be Between 18 and 60")]
        public int Age { get; init; }
        [Required(ErrorMessage = "Position is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Position is 20 characters.")]
        public string? Position { get; init; }
    }
    public class EmployeeDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
        public string Position { get; init; }
    };
    public record EmployeeCreationDto :EmployeeForManipulationDto;
    public record EmployeeForUpdateDto :EmployeeForManipulationDto;
  
}
