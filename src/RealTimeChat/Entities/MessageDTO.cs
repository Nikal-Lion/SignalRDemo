using System.Text;
using System.Data;
using System.Runtime;
using System;

namespace RealTimeChat.Entities
{
    public class MessageDTO
    {
        public string Id { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }
        public string Detail { get; set; }
        public string ToUser { get; set; }
        public string Type { get; set; }
        public bool PushCorpWeixin { get; set; }
    }
}