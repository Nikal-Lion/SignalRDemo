using System.Text;
using System;
namespace RealTimeChat.Entities
{
    public class PushMessageDTO
    {
        public string Id { get; set; }
        public DateTime CreateTime { get; set; }
        public string Detail { get; set; }
        public string Name { get; set; }
        public string ToUser { get; set; }
        public string Type { get; set; }
        public DateTime ReadTime { get; set; }
    }
}