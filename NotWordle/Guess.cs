using System.ComponentModel.DataAnnotations;

namespace NotWordle
{
    public class Guess
    {
        [Required, MaxLength(5), MinLength(5)]
        public string Value { get; set; } = "";
    }
}
