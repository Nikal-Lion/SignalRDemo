using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace DB.Core.Entities
{
    public class User : IdentityUser
    {
        /// <summary>
        /// 
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Profile { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string GitHub { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TopicCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TopicReplyCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateOn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LastTime { get; set; }
    }
}
