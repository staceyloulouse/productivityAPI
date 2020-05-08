using System;

namespace SendNotice.Dtos
{
    public class NoticeReadDto
    {
        public int Id { get; set;}
        public DateTime TimeStamp { get; set;}
        public string  Message {get; set;}
        public string Phone { get; set;}
    }
}