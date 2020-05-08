using System;
using System.ComponentModel.DataAnnotations;

namespace SendNotice.Dtos
{
    public class NoticeCreateDto
    {
        [Required]
        public string  Message {get; set;}
        [Required]
        public string Phone { get; set;}
    }
}