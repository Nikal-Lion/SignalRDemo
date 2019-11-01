using System;

namespace DB.Core.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Message 
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReadTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Detail { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ToUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Type { get; set; }
    }
}