using System.ComponentModel.DataAnnotations;

namespace SendNotice.Dtos
{
    public class UnitUpdateDto
    {
        [Required]
        public string  Name {get; set;}
        [Required]
        public string Phone { get; set;}

    }
}
