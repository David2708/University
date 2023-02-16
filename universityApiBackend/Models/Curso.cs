using System.ComponentModel.DataAnnotations;

namespace universityApiBackend.Models
{
    public class Curso
    {
        public enum Levels
        {
            Basic, Intermidiate, Advanced
        }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string ShortDescription { get; set; } = string.Empty;

        [Required]
        public string LongDescription { get; set; } = string.Empty;

        [Required]
        public string TargetAudiences { get; set; } = string.Empty;

        [Required]
        public string Goals { get; set; } = string.Empty;

        [Required]
        public string Requirements { get; set; } = string.Empty;

        [Required]
        public Levels Level { get; set; }
    }
}
